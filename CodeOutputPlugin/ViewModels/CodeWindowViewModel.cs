﻿using CodeOutputPlugin.Models;
using Gum.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeOutputPlugin.ViewModels
{
    public enum WhatToView
    {
        SelectedElement,
        SelectedState
    }

    public class CodeWindowViewModel : ViewModel
    {
        public WhatToView WhatToView
        {
            get => Get<WhatToView>();
            set => Set(value);
        }

        public bool IsCodeGenPluginEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsShowCodegenPreviewChecked
        {
            get => Get<bool>();
            set => Set(value);
        }

        [DependsOn(nameof(IsShowCodegenPreviewChecked))]
        public Visibility CodePreviewWindowVisibility => IsShowCodegenPreviewChecked.ToVisibility();

        [DependsOn(nameof(WhatToView))]
        public bool IsSelectedObjectSelected
        {
            get => WhatToView == WhatToView.SelectedElement;
            set
            {
                if(value)
                {
                    WhatToView = WhatToView.SelectedElement;
                }
            }
        }

        [DependsOn(nameof(WhatToView))]
        public bool IsSelectedStateSelected
        {
            get => WhatToView == WhatToView.SelectedState;
            set
            {
                if (value)
                {
                    WhatToView = WhatToView.SelectedState;
                }
            }
        }

        public InheritanceLocation InheritanceLocation
        {
            get => Get<InheritanceLocation>();
            set => Set(value);
        }

        [DependsOn(nameof(InheritanceLocation))]
        public bool IsInCustomCodeChecked
        {
            get => InheritanceLocation == InheritanceLocation.InCustomCode;
            set
            {
                if (value)
                {
                    InheritanceLocation = InheritanceLocation.InCustomCode;
                }
            }
        }

        [DependsOn(nameof(InheritanceLocation))]
        public bool IsInGeneratedCodeChecked
        {
            get => InheritanceLocation == InheritanceLocation.InGeneratedCode;
            set
            {
                if (value)
                {
                    InheritanceLocation = InheritanceLocation.InGeneratedCode;
                }
            }
        }

        public string Code
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
