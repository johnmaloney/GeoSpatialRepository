using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public abstract class AIndexCell<T>
    {
        #region Fields



        #endregion

        #region Properties



        #endregion

        #region Methods

        protected static SingleCellXY CellIndex(RectangleF bounds, double cellx1, double celly1, double cellx2, double celly2)
        {
            double cellxm = (cellx1 + cellx2) * 0.5;
            double cellym = (celly1 + celly2) * 0.5;

            var cell = new SingleCellXY
            {
                X = -1,
                Y = -1
            };

            if (bounds.Right < cellxm)
                cell.X = 0;
            else if (bounds.Left > cellxm)
                cell.X = 1;

            if (bounds.Bottom < cellym)
                cell.Y = 0;
            else if (bounds.Top > cellym)
                cell.Y = 1;

            return cell;
        }

        #endregion
    }

    public static class IndexCellExtensions
    {
        
    }
}
