using Microsoft.AspNetCore.Components;
using System;

namespace imgconv.Components
{
    public interface IMetaData
    {
        string Name { get; set; }
        string Value { get; set; }
        string InputType { get; set; }
        MetaType Type { get; set; }
    }

    public enum MetaType
    {
        Px,
        Dpi,
        Rotation,
        Cm,
        Title,
        Alt,
    }

    public partial class MetadataField(bool isEditing)
    {
        [Parameter]
        public IMetaData Meta { get; set; }

        private string? oldValue = null;
        private bool isEditing = isEditing;

        private void EditValue()
        {
            oldValue = Meta.Value;
            isEditing = true;
        }

        private void ResetValue()
        {
            Meta.Value = oldValue;
            isEditing = false;
        }

        private void SaveValue()
        {
            // Validate the value based on the type
            switch (Meta.Type)
            {
                case MetaType.Px:
                case MetaType.Dpi:
                case MetaType.Rotation:
                case MetaType.Cm:
                    Meta.InputType = "number";
                    if (!float.TryParse(Meta.Value, out _))
                    {
                        // Display an error message or handle the invalid value
                        // For example: Show a validation error message
                    }
                    break;
                case MetaType.Title:
                case MetaType.Alt:
                    Meta.InputType = "text";
                    // No validation needed for string types
                    break;
                default:
                    Meta.InputType = "text";
                    // Handle unknown type
                    break;
            }

            isEditing = false;
        }
    }
}
