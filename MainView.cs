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
            cb_language.SelectedIndex = 0;
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
                    var koKR_mem = (ulong)m.Get64BitCode($"{GameName}+{Offsets[i]},{Pointers[i]}");
                    Console.WriteLine(koKR_mem);
                    var r = m.WriteMemory(koKR_mem.ToString("X"), "string", cb_language.Text);
                    var r2 = m.WriteMemory($"{(koKR_mem + 0xC0).ToString("X")}", "string", cb_language.Text);
                    if (r && r2)
                    {
                        txt_status.Text += $"已修改 {koKR_mem} 為 {cb_language.Text}\r\n";
                        txt_status.Text += $"已修改 {koKR_mem + 0xC0} 為 {cb_language.Text}\r\n";
                    }
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
