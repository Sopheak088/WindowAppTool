using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsForms.Test
{
    public partial class FrmGridCombo : Form
    {
        public FrmGridCombo()
        {
            InitializeComponent();
            DataTable ds = new DataTable();
            ds = GridComboDao.ListUser();
            gridEX1.DropDowns["ddUser"].SetDataBinding(ds, "");
            //gridEX1.DropDowns["ddUser"].DataSource = ds;
            gridEX1.RootTable.Columns["UserName"].DropDown = gridEX1.DropDowns["ddUser"];
            gridEX1.DropDowns["ddUser"].DataMember = "ID";
            int i = ds.Rows.Count;
            //gridEX1.RootTable.Columns["UserName"].EditType = EditType.NoEdit;
            // gridEX1.AllowAddNew = InheritableBoolean.True;
            //gridEX1.AllowDelete = InheritableBoolean.True;
            //gridEX1.AllowEdit = InheritableBoolean.True;
            //gridEX1.NewRowPosition = NewRowPosition.BottomRow;
            // gridEX1.EmptyRows = true;
            // gridEX1.CellSelectionMode = CellSelectionMode.EntireRow;
            // gridEX1.UpdateMode = UpdateMode.CellUpdate;
            // gridEX1.TotalRow = InheritableBoolean.True;
        }
    }
}