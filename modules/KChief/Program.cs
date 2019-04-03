/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Dolittle.Edge.Modules.Booting;

namespace Dolittle.Edge.Terasaki
{
    class Program
    {
        static void Main(string[] args)
        {
            Bootloader.Configure(_ => {}).Start().Wait();
        }
    }
}