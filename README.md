# Report Highlighter

This C# and XAML project, Report Highlighter, is a Windows Presentation Foundation (WPF) application. It highlights a set of fields that are associated with the report currently being hovered over.

## Functionality
The Report Highlighter application includes a MainWindow with the following features:

- Pre-populates a set of ComboBoxes with some example values. This happens during the initialization of the MainWindow.
- Implements a HighlightBehavior for labels. When a label's highlight property changes (which includes an array of field names), it registers event handlers for MouseEnter and MouseLeave events that trigger a change in background color for all the associated fields.
## Classes and Methods
There are two main classes involved in this application:

1. MainWindow: This application’s primary window contains ComboBoxes populated with values during initialization.

2. HighlightBehavior: This static class provides behavior for highlighting UI controls in a WPF application. The class has a DependencyProperty Highlight, which stores the field names to be highlighted. It uses a ConcurrentDictionary to keep the relationship between field names and their corresponding controls for thread safety. It also provides methods to update the field-control map recursively, set field backgrounds, remove controls from the dictionary, and clear the dictionary.

## Example
Two labels, Report_1 and Report_2, have been defined in the XAML code with their HighlightBehavior.Highlight properties bound to the ReportFieldNames1 and ReportFieldNames2 arrays, respectively. When the mouse enters and leaves their area, these labels will trigger a highlight effect on the fields defined in their associated string arrays.

![](https://github.com/Austin-Tracy/ReportHighlighter/blob/main/Highlight.gif)
```xml
<Label x:Name="Report_1" Grid.Column="1" Content="Report 1" WpfApp1:HighlightBehavior.Highlight="{Binding ReportFieldNames1}" HorizontalAlignment="Left" Margin="611,38,0,0" VerticalAlignment="Top" Height="28" Width="145"/>
<Label x:Name="Report_2" Grid.Column="1" Content="Report 2" WpfApp1:HighlightBehavior.Highlight="{Binding ReportFieldNames2}" HorizontalAlignment="Left" Margin="611,69,0,0" VerticalAlignment="Top" Height="28" Width="145"/>
```

## Future Work
This project is a minimal example, some ideas to expand this functionality.

- The class-level arrays of strings in MainWindow can be replaced with functions to fetch real data from a database or a file.
- The HighlightBehavior class could be extended to support more UI controls.
- Add additional behaviors to define interactions multiple elements.

## License
This project is licensed under MIT License terms.
