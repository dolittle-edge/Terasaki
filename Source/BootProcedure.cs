/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.IO;
using Dolittle.Booting;
using Dolittle.TimeSeries.Modules;
using Newtonsoft.Json;

namespace Dolittle.TimeSeries.Terasaki
{
    /// <summary>
    /// Represents the <see cref="ICanPerformBootProcedure">boot procedure</see> that initializes the module
    /// </summary>
    public class BootProcedure : ICanPerformBootProcedure
    {
        readonly ICoordinator _coordinator;

        /// <summary>
        /// Initializes a new instance of <see cref="BootProcedure"/>
        /// </summary>
        /// <param name="coordinator"><see cref="ICoordinator"/> to initialize</param>
        public BootProcedure(ICoordinator coordinator)
        {
            _coordinator = coordinator;
        }

        /// <inheritdoc/>
        public bool CanPerform() => true;

        /// <inheritdoc/>
        public void Perform()
        {
            _coordinator.Initialize();
           
        }
    }
}