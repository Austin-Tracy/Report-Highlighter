using System.Collections.Concurrent;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
//Example XAML for a Label, placed in MainWindow.xaml:
//  <Label x:Name="Report_1" Content="Report 1" WpfApp1:HighlightBehavior.Highlight="{Binding ReportFieldNames1}"/>
//Example string array ReportFieldNames2, placed in MainWindow.xaml.cs:
//  public string[] ReportFieldNames2 { get; } = {"FirstName", "LastName", "Title", "Address1", "Middle", "Agency", "State", "Email"};
namespace WpfApp1
{
    // This class provides a behavior for highlighting UI controls in a WPF application.
    public static class HighlightBehavior
    {
        // The attached DependencyProperty that stores the field names to be highlighted.
        public static readonly DependencyProperty HighlightProperty = DependencyProperty.RegisterAttached(
            "Highlight",
            typeof(string[]),
            typeof(HighlightBehavior),
            new PropertyMetadata(null, OnHighlightChanged));
        // ConcurrentDictionary(Thread-Safety) to store the relationship between field names and their corresponding controls.
        private static readonly ConcurrentDictionary<string, Control> _fieldControlMap = new ConcurrentDictionary<string, Control>();
        // Getter for the Highlight attached property.
        public static string[] GetHighlight(DependencyObject obj)
        {
            return (string[])obj.GetValue(HighlightProperty);
        }
        // Setter for the Highlight attached property.
        public static void SetHighlight(DependencyObject obj, string[] value)
        {
            obj.SetValue(HighlightProperty, value);
        }
        // Event handler for when the Highlight attached property changes.
        private static void OnHighlightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Control control)
            {
                // Add event handlers for MouseEnter and MouseLeave events.
                control.MouseEnter += (sender, args) => SetFieldBackgrounds((string[])e.NewValue, Brushes.LightBlue);
                control.MouseLeave += (sender, args) => SetFieldBackgrounds((string[])e.NewValue, Brushes.White);

                // Update the field control map with the new field names.
                UpdateFieldControlMap(Application.Current.MainWindow, (string[])e.NewValue);
            }
        }
        // Updates the _fieldControlMap dictionary with the provided field names.
        private static void UpdateFieldControlMap(DependencyObject parent, string[] fieldNames)
        {
            foreach (string fieldName in fieldNames)
            {
                // If the field name is not already in the dictionary, add it.
                if (!_fieldControlMap.ContainsKey(fieldName))
                {
                    Control? field = FindControlRecursive(parent, fieldName);
                    if (field != null) 
                    {
                        _fieldControlMap.TryAdd(fieldName, field);
                    }
                }
            }
        }
        // Searches for a control with the specified 'controlName' within the visual tree.
        // It performs a depth-first search, starting from the 'parent' DependencyObject.
        private static Control? FindControlRecursive(DependencyObject parent, string controlName)
        {
            // Get the number of children for the current 'parent' DependencyObject.
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                // Get the child at the current index.
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                // Check if the child is a Control and if its name matches the 'controlName'.
                if (child is Control control && control.Name == controlName)
                {
                    // If a match is found, return the control.
                    return control;
                }
                // If the child is not a match, continue the search recursively for its children.
                Control? foundControl = FindControlRecursive(child, controlName);

                if (foundControl != null)
                {
                    return foundControl;
                }
            }
            // If the control is not found in the 'parent' subtree, return null
            return null;
        }
        // Sets the background color of the fields specified in the fieldNames array.
        private static void SetFieldBackgrounds(string[] fieldNames, Brush background)
        {
            foreach (string fieldName in fieldNames)
            {
                // If the field name exists in the _fieldControlMap dictionary, set the background color.
                if (_fieldControlMap.TryGetValue(fieldName, out Control field))
                {
                    if (field is TextBox || field is ComboBox)
                    {
                        field.Background = background;
                    }
                    else if (field is CheckBox box)
                    {
                        box.Background = background;
                    }
                }
            }
        }
        // Removes controls from the dictionary when they are removed from the visual tree or their names are changed.
        // Possible Use Cases:
        //   1. When a Control is removed from the visual tree at runtime,
        //      call this method to remove corresponding control form dictionary.
        //   2. When a Control's name has changed during runtime, call this method
        //      to remove the old name from the dictionary before updating the controls name.
        //      After updating the control's name, call UpdateFieldControlMap() to add the
        //      the control with the new name to the dictionary.
        public static void RemoveControlFromFieldControlMap(string fieldName)
        {
            if (_fieldControlMap.ContainsKey(fieldName))
            {
                _fieldControlMap.TryRemove(fieldName, out _);
            }
        }
        // Clears the '_fieldControlMap' dictionary when appropriate.
        // Possible Use Cases:
        //   1. When Main Window is closed and controls are no longer needed.
        //   2. Dynamic UI where controls are frequently added and removed,
        //      use this method to clean up dictionary and avoid stale references.
        public static void ClearFieldControlMap()
        {
            _fieldControlMap.Clear();
        }
    }
}
