/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using Dolittle.Booting;

namespace Dolittle.Edge.Terasaki
{
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly ICoordinator _connector;

        public BootProcedure(ICoordinator connector)
        {
            _connector = connector;
        }

        public bool CanPerform() => true;

        public void Perform()
        {
            _connector.Initialize();
        }
    }
}