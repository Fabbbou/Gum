﻿using Gum.Plugins.BaseClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gum.Plugins.Undos
{
    // I started working on this but then realized that the undos don't store off just the variables that changed, but the entire object...
    // So it's a pain to identify exactly what has changed to list it.

    [Export(typeof(PluginBase))]
    public class MainPlugin : InternalPlugin
    {
        public override void StartUp()
        {
            var control = new UndoDisplay();

            control.DataContext = new UndosViewModel();

            GumCommands.Self.GuiCommands.AddControl(control, "Undos", TabLocation.Right);

        }
    }
}
