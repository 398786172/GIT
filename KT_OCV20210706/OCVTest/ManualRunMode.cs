using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OCV.OCVTest
{
    public partial class ManualRunMode : Form
    {
        public ManualRunMode()
        {
            InitializeComponent();
        }

        private void Btn_A_RUN_Click_2(object sender, EventArgs e)
        {
            var autoForm = this.Owner as FrmSys;
            autoForm.Btn_A_RUN_Click(sender, e);
        }

        private void btnRunMode_Click(object sender, EventArgs e)
        {
            var autoForm = this.Owner as FrmSys;
            autoForm.btnRunMode_Click(sender, e);

        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            var autoForm = this.Owner as FrmSys;
            autoForm.btnHome_Click(sender, e);
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            var autoForm = this.Owner as FrmSys;
            autoForm.skinButton2_Click(sender, e);
        }

        private void Btn_A_stop_Click(object sender, EventArgs e)
        {
            var autoForm = this.Owner as FrmSys;
            autoForm.Btn_A_stop_Click(sender, e);
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            var autoForm = this.Owner as FrmSys;
            autoForm.skinButton1_Click(sender, e);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string path = Application.StartupPath + @"\Setting\ManulOperation.ini";
            OCVTypetxt.Text = INIAPI.INIGetStringValue(path, "System", "OCVType", null);
            Cell1Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL1MODEL", null);
            Cell2Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL2MODEL", null);
            Cell3Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL3MODEL", null);
            Cell4Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL4MODEL", null);
            Cell5Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL5MODEL", null);
            Cell6Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL6MODEL", null);
            Cell7Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL7MODEL", null);
            Cell8Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL8MODEL", null);
            Cell9Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL9MODEL", null);
            Cell10Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL10MODEL", null);
            Cell11Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL11MODEL", null);
            Cell12Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL12MODEL", null);
            Cell13Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL13MODEL", null);
            Cell14Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL14MODEL", null);
            Cell15Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL15MODEL", null);
            Cell16Modeltxt.Text = INIAPI.INIGetStringValue(path, "System", "CELL16MODEL", null);
            Cell1BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL1BARCODE", null);
            Cell2BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL2BARCODE", null);
            Cell3BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL3BARCODE", null);
            Cell4BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL4BARCODE", null);
            Cell5BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL5BARCODE", null);
            Cell6BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL6BARCODE", null);
            Cell7BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL7BARCODE", null);
            Cell8BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL8BARCODE", null);
            Cell9BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL9BARCODE", null);
            Cell10BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL10BARCODE", null);
            Cell11BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL11BARCODE", null);
            Cell12BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL12BARCODE", null);
            Cell13BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL13BARCODE", null);
            Cell14BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL14BARCODE", null);
            Cell15BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL15BARCODE", null);
            Cell16BarCode.Text = INIAPI.INIGetStringValue(path, "System", "CELL16BARCODE", null);
        }

        private void BatterySaveBtn_Click(object sender, EventArgs e)
        {
            ClsGlobal.listETCELL = new List<ET_CELL>()
                {
                    new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                    ,new ET_CELL()
                };
            ClsGlobal.listETCELL[0].Cell_ID = Cell1BarCode.Text;
            ClsGlobal.listETCELL[1].Cell_ID = Cell2BarCode.Text;
            ClsGlobal.listETCELL[2].Cell_ID = Cell3BarCode.Text;
            ClsGlobal.listETCELL[3].Cell_ID = Cell4BarCode.Text;
            ClsGlobal.listETCELL[4].Cell_ID = Cell5BarCode.Text;
            ClsGlobal.listETCELL[5].Cell_ID = Cell6BarCode.Text;
            ClsGlobal.listETCELL[6].Cell_ID = Cell7BarCode.Text;
            ClsGlobal.listETCELL[7].Cell_ID = Cell8BarCode.Text;
            ClsGlobal.listETCELL[8].Cell_ID = Cell9BarCode.Text;
            ClsGlobal.listETCELL[9].Cell_ID = Cell10BarCode.Text;
            ClsGlobal.listETCELL[10].Cell_ID = Cell11BarCode.Text;
            ClsGlobal.listETCELL[11].Cell_ID = Cell12BarCode.Text;
            ClsGlobal.listETCELL[12].Cell_ID = Cell13BarCode.Text;
            ClsGlobal.listETCELL[13].Cell_ID = Cell14BarCode.Text;
            ClsGlobal.listETCELL[14].Cell_ID = Cell15BarCode.Text;
            ClsGlobal.listETCELL[15].Cell_ID = Cell16BarCode.Text;
            ClsGlobal.listETCELL[0].MODEL_NO = Cell1Modeltxt.Text;
            ClsGlobal.listETCELL[1].MODEL_NO = Cell2Modeltxt.Text;
            ClsGlobal.listETCELL[2].MODEL_NO = Cell3Modeltxt.Text;
            ClsGlobal.listETCELL[3].MODEL_NO = Cell4Modeltxt.Text;
            ClsGlobal.listETCELL[4].MODEL_NO = Cell5Modeltxt.Text;
            ClsGlobal.listETCELL[5].MODEL_NO = Cell6Modeltxt.Text;
            ClsGlobal.listETCELL[6].MODEL_NO = Cell7Modeltxt.Text;
            ClsGlobal.listETCELL[7].MODEL_NO = Cell8Modeltxt.Text;
            ClsGlobal.listETCELL[8].MODEL_NO = Cell9Modeltxt.Text;
            ClsGlobal.listETCELL[9].MODEL_NO = Cell10Modeltxt.Text;
            ClsGlobal.listETCELL[10].MODEL_NO = Cell11Modeltxt.Text;
            ClsGlobal.listETCELL[11].MODEL_NO = Cell12Modeltxt.Text;
            ClsGlobal.listETCELL[12].MODEL_NO = Cell13Modeltxt.Text;
            ClsGlobal.listETCELL[13].MODEL_NO = Cell14Modeltxt.Text;
            ClsGlobal.listETCELL[14].MODEL_NO = Cell15Modeltxt.Text;
            ClsGlobal.listETCELL[15].MODEL_NO = Cell16Modeltxt.Text;
            ClsGlobal.OCVType = int.Parse(OCVTypetxt.Text.Substring(3, 1));
            string path = Application.StartupPath + @"\Setting\ManulOperation.ini";
            INIAPI.INIWriteValue(path, "System", "OCVType", OCVTypetxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL1MODEL", Cell1Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL2MODEL", Cell2Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL3MODEL", Cell3Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL4MODEL", Cell4Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL5MODEL", Cell5Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL6MODEL", Cell6Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL7MODEL", Cell7Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL8MODEL", Cell8Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL9MODEL", Cell9Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL10MODEL", Cell10Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL11MODEL", Cell11Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL12MODEL", Cell12Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL13MODEL", Cell13Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL14MODEL", Cell14Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL15MODEL", Cell15Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL16MODEL", Cell16Modeltxt.Text);
            INIAPI.INIWriteValue(path, "System", "CELL1BARCODE", Cell1BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL2BARCODE", Cell2BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL3BARCODE", Cell3BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL4BARCODE", Cell4BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL5BARCODE", Cell5BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL6BARCODE", Cell6BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL7BARCODE", Cell7BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL8BARCODE", Cell8BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL9BARCODE", Cell9BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL10BARCODE", Cell10BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL11BARCODE", Cell11BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL12BARCODE", Cell12BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL13BARCODE", Cell13BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL14BARCODE", Cell14BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL15BARCODE", Cell15BarCode.Text);
            INIAPI.INIWriteValue(path, "System", "CELL16BARCODE", Cell16BarCode.Text);

        }
    }
}
