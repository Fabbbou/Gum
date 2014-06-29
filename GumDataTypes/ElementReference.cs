﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ToolsUtilities;

namespace Gum.DataTypes
{
    public enum ElementType
    {
        Screen,
        Component,
        Standard
    }

    public class ElementReference
    {
        public const string ScreenSubfolder = "Screens";
        public const string ComponentSubfolder = "Components";
        public const string StandardSubfolder = "Standards";

        public ElementType ElementType
        {
            get;
            set;
        }

        string Subfolder
        {
            get
            {
                switch (ElementType)
                {
                    case DataTypes.ElementType.Standard:
                        return StandardSubfolder;
                    case DataTypes.ElementType.Component:
                        return ComponentSubfolder;
                    case DataTypes.ElementType.Screen:
                        return ScreenSubfolder;
                }
                throw new InvalidOperationException();
            }
        }

        public string Name;

        public ElementSave ToElementSave(string projectroot, string extension)
        {
            string fullName = projectroot + Subfolder + "/" + Name + "." + extension;

            ElementSave elementSave = FileManager.XmlDeserialize<ElementSave>(fullName);

            return elementSave;
        }


        public T ToElementSave<T>(string projectroot, string extension, ref string errors) where T : ElementSave, new()
        {
            string fullName = projectroot + Subfolder + "/" + Name + "." + extension;

            if (ToolsUtilities.FileManager.IsRelative(fullName))
            {
                fullName = ToolsUtilities.FileManager.RelativeDirectory + fullName;
            }
            fullName = ToolsUtilities.FileManager.Standardize(fullName);

            if (System.IO.File.Exists(fullName))
            {


                T elementSave = FileManager.XmlDeserialize<T>(fullName);

                if (Name != elementSave.Name)
                {
                    // The file name doesn't match the name of the element.  This can cause errors
                    // at runtime so let's tell the user:
                    errors += "\nThe project references an element named " + Name + ", but the XML for this element has its name set to " + elementSave.Name + "\n"; 
                }

                return elementSave;
            }
            else
            {
                // I don't think we want to consider this an error anymore
                // because Gum can handle it - it doesn't allow saving that 
                // individual element and it shows a red ! next to the element.
                // We should just tolerate this and let the user deal with it.
                // If we do treat this as an error, then Gum goes into a state 
                // where it can't save anything.
                //errors += "\nCould not find the file name " + fullName;

                T elementSave = new T();

                elementSave.Name = Name;
                elementSave.IsSourceFileMissing = true;

                return elementSave;
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }



}
