/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Machine.Specifications;
using Moq;
using It = Machine.Specifications.It;
using System;
using Dolittle.Edge.Modules;

namespace Dolittle.Edge.Terasaki.for_Coordinator
{
    public class when_channel_is_received
    {
        static Mock<ICommunicationClient> communication_client;
        static Mock<IConnector> connector;
        static Coordinator coordinator;
        static Action<Channel> callback;

        static Channel channel;
        static TagDataPoint<ChannelValue> data_point;

        Establish context = () =>
        {
            communication_client = new Mock<ICommunicationClient>();
            connector = new Mock<IConnector>();

            connector
                .Setup(_ => _.Subscribe(Moq.It.IsAny<Action<Channel>>())).Callback((Action<Channel> _) => callback = _);

            communication_client
                .Setup(_ => _.SendAsJson(Moq.It.IsAny<Output>(), Moq.It.IsAny<object>()))
                .Callback((Output output, object dataPoint) => data_point = (TagDataPoint<ChannelValue>)dataPoint);

            channel = new Channel
            {
                Id = 1337,
                Value = new ChannelValue
                {
                Value = 42.43,
                State = 'R',
                ParityError = true
                }
            };

            coordinator = new Coordinator(communication_client.Object, connector.Object);
            coordinator.Initialize();
        };

        Because of = () => callback(channel);

        It should_send_a_data_point_with_expected_tag = () => data_point.Tag.Value.ShouldEqual(channel.Id.ToString());
        It should_send_a_data_point_with_expected_control_system = () => data_point.ControlSystem.Value.ShouldEqual(Coordinator.ControlSystemName);
        It should_send_a_data_point_with_a_timestamp = () => data_point.Timestamp.ShouldBeGreaterThan(0);
        It should_send_a_data_point_with_expected_value = () => data_point.Value.Value.ShouldEqual(channel.Value.Value);
        It should_send_a_data_point_with_expected_state = () => data_point.Value.State.ShouldEqual(channel.Value.State);
        It should_send_a_data_point_with_expected_parity_status = () => data_point.Value.ParityError.ShouldEqual(channel.Value.ParityError);
    }
}