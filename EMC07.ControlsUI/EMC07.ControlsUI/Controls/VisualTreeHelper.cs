using Avalonia;
using Avalonia.Controls;

public static class VisualTreeHelper
{
    public static int GetChildrenCount(this AvaloniaObject control)
    {
        int childrenCount = 0;

            foreach (var visualChild in Avalonia.VisualTree.VisualExtensions.GetVisualChildren((Control)control))
            {
                if (visualChild is Control)
                {
                    childrenCount++;
                }
            }
        

        return childrenCount;
    }
    public static AvaloniaObject? GetChildWithIndex(AvaloniaObject visual, int index)
    {
        if ((Visual)visual is Visual visualWithChildren)
        {
            if (visualWithChildren is Panel panel)
            {
                if (index < panel.Children.Count)
                {
                    return panel.Children[index];
                }
            }
            else if (index == 0)
            {
                return visualWithChildren;
            }
        }
        return null;
    }
 
}
