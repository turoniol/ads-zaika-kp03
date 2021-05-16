using System.Drawing;
using System.Drawing.Drawing2D;
using BinaryTrees;
using System.Collections.Generic;

public static class TreeVisualiser
{
    private static readonly int _radius = 10;
    private static bool _isFirst;

    public static void Draw(RBTree tree, string fileName, int beginX, int beginY, int shiftX, int shiftY)
    {
        Bitmap bmp = new Bitmap(1200, 900);
        Graphics gr = Graphics.FromImage(bmp);
        Rectangle rect = new Rectangle(0, 0, 1200, 900);
        gr.FillRectangle(Brushes.RoyalBlue, rect);

        _isFirst = true;
        DrawEdge(gr, tree, beginX - shiftX, beginY - shiftY, shiftX, shiftY);
        DrawRoot(gr, tree, beginX - shiftX, beginY - shiftY, shiftX, shiftY);
        gr.Dispose();
        
        bmp.Save(fileName);
        bmp.Dispose();
    }

    private static void DrawEdge(Graphics gr, RBTree tree, int parentX, int parentY, int shiftX, int shiftY)
    {
        if (tree.IsEmpty()) return;

        int currentX = parentX + shiftX;
        int currentY = parentY + shiftY;

        if (!_isFirst)
        {
            Pen linePen = new Pen(Brushes.Gray, 2);
            gr.DrawLine(linePen, parentX, parentY + _radius, currentX, currentY + _radius);
            linePen.Dispose();
        }
        _isFirst = false;

        if (tree.Root.Left != null)
        {
            DrawEdge(gr, tree.Root.Left, currentX, currentY, -(int) System.Math.Abs(shiftX) / 2, shiftY);
        }

        if (tree.Root.Right != null)
        {
            DrawEdge(gr, tree.Root.Right, currentX, currentY, (int) System.Math.Abs(shiftX) / 2, shiftY);
        }
    }

    private static void DrawRoot(Graphics gr, RBTree tree, int parentX, int parentY, int shiftX, int shiftY)
    {
        if (tree.IsEmpty()) return;

        int currentX = parentX + shiftX;
        int currentY = parentY + shiftY;

        Rectangle rect = new Rectangle(currentX - _radius, currentY, 2 * _radius, 2 * _radius);

        var brush = tree.Root.IsBlack() ? Brushes.Black : Brushes.Red;
        gr.FillEllipse(brush, rect);
        Pen ellipsePen = new Pen(brush, _radius);
        gr.DrawEllipse(ellipsePen, rect);
        gr.DrawString(tree.Root.Value.ToString(), new Font("Arial Black", 10), 
            Brushes.White, currentX - _radius/2, currentY);
        ellipsePen.Dispose();

        if (tree.Root.Left != null)
        {
            DrawRoot(gr, tree.Root.Left, currentX, currentY, -(int) System.Math.Abs(shiftX) / 2, shiftY);
        }

        if (tree.Root.Right != null)
        {
            DrawRoot(gr, tree.Root.Right, currentX, currentY, (int) System.Math.Abs(shiftX) / 2, shiftY);
        }
    }
}