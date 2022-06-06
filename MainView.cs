using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory;


namespace DiabloImmortalToZHTW
{
    public partial class MainView : Form
    {
        public Mem m = new Mem();
        public const string GameName = "DiabloImmortal.exe";
        public List<string> Offsets = new List<string>() { "0x03D37000", "0x03F5E230", "0x03D8DA30" };
        public List<string> Pointers = new List<string>() { "0x1A8", "0x1D8", "0xE8" };


        public MainView()
        {
            InitializeComponent();
            JustDoIt();
        }

        void JustDoIt()
        {
            try
            {
                txt_status.Clear();
                m.OpenProcess(GameName);
                for (int i = 0; i < Offsets.Count; i++)
                {
                    var koKR_mem = m.Get64BitCode($"{GameName}+{Offsets[i]},{Pointers[i]}");
                    m.WriteMemory($"{koKR_mem}", "string", "zhTW");
                    m.WriteMemory($"{koKR_mem + 0xC0}", "string", "zhTW");
                    txt_status.Text += $"已修改 {koKR_mem} 為 zhTW\r\n";
                    txt_status.Text += $"已修改 {koKR_mem + 0xC0} 為 zhTW\r\n";
                }
            }
            catch (Exception ex)
            {
                txt_status.Text = $"修改失敗\r\n{ex}";
            }
        }

        private void btn_write_Click(object sender, EventArgs e)
        {
            JustDoIt();
        }
    }
}
