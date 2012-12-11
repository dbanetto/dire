using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace dire.gui
//http://dotnetrix.co.uk/tabcontrol.htm#tip7
//Edit - removed TabDragOut as it is not required
{
    public class TabDragger
    {
    public TabDragger(TabControl tabControl)
        : base()
    {
        this.tabControl = tabControl;
        tabControl.MouseDown +=new MouseEventHandler(tabControl_MouseDown);
        tabControl.MouseMove += new MouseEventHandler(tabControl_MouseMove);
        tabControl.DoubleClick += new EventHandler(tabControl_DoubleClick);
    }

    public TabDragger(TabControl tabControl, TabDragBehavior behavior) 
        : this(tabControl)
    {
        this.dragBehavior = behavior;
    }

    private TabControl tabControl;
    private TabPage dragTab = null;
    private TabDragBehavior dragBehavior = TabDragBehavior.TabDragArrange;

    private TabDragBehavior DragBehavior
    {
        get
        {
            if (!tabControl.Multiline)
                return dragBehavior;
            return TabDragBehavior.None;
        }
    }

    private void tabControl_MouseDown(object sender, MouseEventArgs e)
    {
        dragTab = TabUnderMouse();
    }

    private void tabControl_MouseMove(object sender, MouseEventArgs e)
    {
        if (DragBehavior == TabDragBehavior.None)
            return;

        if (e.Button == MouseButtons.Left)
        {
            if (dragTab != null)
            {
                if (tabControl.TabPages.Contains(dragTab))
                {
                    if (PointInTabStrip(e.Location))
                    {
                        TabPage hotTab = TabUnderMouse();
                        if (hotTab != dragTab && hotTab != null)
                        {
                            int id1 = tabControl.TabPages.IndexOf(dragTab);
                            int id2 = tabControl.TabPages.IndexOf(hotTab);
                            if (id1 > id2)
                            {
                                for (int id = id2; id <= id1; id++)
                                {
                                    SwapTabPages(id1, id);
                                }
                            }
                            else
                            {
                                for (int id = id2; id > id1; id--)
                                {
                                    SwapTabPages(id1, id);
                                }
                            }
                            tabControl.SelectedTab = dragTab;
                        }
                    }
                    else
                    {
                        
                    }
                }
            }
        }
    }

    private void tabControl_DoubleClick(object sender, EventArgs e)
    {
        
    }

    #region Private Methods

    private TabPage TabUnderMouse()
    {
        NativeMethods.TCHITTESTINFO HTI = new NativeMethods.TCHITTESTINFO(tabControl.PointToClient(Cursor.Position));
        int tabID = NativeMethods.SendMessage(tabControl.Handle, NativeMethods.TCM_HITTEST, IntPtr.Zero, ref HTI);
        return tabID == -1 ? null : tabControl.TabPages[tabID];
    }

    private bool PointInTabStrip(Point point)
    {
        Rectangle tabBounds = Rectangle.Empty;
        Rectangle displayRC = tabControl.DisplayRectangle; ;

        switch (tabControl.Alignment)
        {
            case TabAlignment.Bottom:
                tabBounds.Location = new Point(0, displayRC.Bottom);
                tabBounds.Size = new Size(tabControl.Width, tabControl.Height - displayRC.Height);
                break;

            case TabAlignment.Left:
                tabBounds.Size = new Size(displayRC.Left, tabControl.Height);
                break;

            case TabAlignment.Right:
                tabBounds.Location = new Point(displayRC.Right, 0);
                tabBounds.Size = new Size(tabControl.Width - displayRC.Width, tabControl.Height);
                break;

            default:
                tabBounds.Size = new Size(tabControl.Width, displayRC.Top);
                break;
        }
        tabBounds.Inflate(-3, -3);
        return tabBounds.Contains(point);
    }

    private void SwapTabPages(int index1, int index2)
    {
        if ((index1 | index2) != -1)
        {
            TabPage tab1 = tabControl.TabPages[index1];
            TabPage tab2 = tabControl.TabPages[index2];
            tabControl.TabPages[index1] = tab2;
            tabControl.TabPages[index2] = tab1;
        }
    }

    #endregion

}

internal sealed class NativeMethods
{

    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left, Top, Right, Bottom;
        public RECT(Rectangle bounds)
        {
            this.Left = bounds.Left;
            this.Top = bounds.Top;
            this.Right = bounds.Right;
            this.Bottom = bounds.Bottom;
        }
        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}", Left, Top, Right, Bottom);
        }
    }

    public const int WM_NCLBUTTONDBLCLK = 0xA3;
    
    public const int WM_SETCURSOR = 0x20;

    public const int WM_NCHITTEST = 0x84;

    public const int WM_MOUSEMOVE = 0x200;
    public const int WM_MOVING = 0x216;
    public const int WM_EXITSIZEMOVE = 0x232;

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam);

    [DllImport("user32.dll")]
    public static extern int SendMessage(IntPtr hwnd, int msg, IntPtr wParam, ref TCHITTESTINFO lParam);

    [StructLayout(LayoutKind.Sequential)]
    public struct TCHITTESTINFO
    {
        public Point pt;
        public TCHITTESTFLAGS flags;
        public TCHITTESTINFO(Point point)
        {
            pt = point;
            flags = TCHITTESTFLAGS.TCHT_ONITEM;
        }
    }

    [Flags()]
    public enum TCHITTESTFLAGS
    {
        TCHT_NOWHERE = 1,
        TCHT_ONITEMICON = 2,
        TCHT_ONITEMLABEL = 4,
        TCHT_ONITEM = TCHT_ONITEMICON | TCHT_ONITEMLABEL
    }

    public const int TCM_HITTEST = 0x130D;

}

public enum TabDragBehavior
{None, TabDragArrange }
}
