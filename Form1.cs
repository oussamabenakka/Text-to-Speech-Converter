using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech;

namespace Text_to_Speech_Converter
{
    public partial class Form1 : Form
    {
        SpeechSynthesizer speech;
        public Form1()
        {
            InitializeComponent();
            speech = new SpeechSynthesizer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button3.Enabled = false;
            trackBarvalum.Value = 100;
            foreach (var voice in speech.GetInstalledVoices())
            {
                comboBox1.Items.Add(voice.VoiceInfo.Name);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text!="")
            {
                if(comboBox1.Text=="")
                {
                    MessageBox.Show("sélection un langue", "", MessageBoxButtons.OK);
                }
                else
                {
                    button2.Enabled = true;
                    button3.Enabled = true;
                    speech.Rate = trackBarSpeed.Value;//set speed
                    speech.Volume = trackBarvalum.Value;//set valume
                    speech.SelectVoice(comboBox1.Text);
                    speech.SpeakAsync(richTextBox1.Text);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (speech.State == SynthesizerState.Speaking)
                speech.Pause();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (speech.State == SynthesizerState.Paused)
                speech.Resume();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text == "")
            {
                MessageBox.Show("Enter your text first", "", MessageBoxButtons.OK);
            }
            else
            {
                SpeechSynthesizer ss = new SpeechSynthesizer();
                SaveFileDialog savefile = new SaveFileDialog();
                savefile.Filter = "Wave Files | *.wav";
                savefile.ShowDialog();
                ss.SetOutputToWaveFile(savefile.FileName);
                ss.Rate = trackBarSpeed.Value;
                ss.Volume = trackBarvalum.Value;
                ss.SelectVoice(comboBox1.Text);
                ss.SpeakAsync(richTextBox1.Text);
                MessageBox.Show("Recording Completed...", "");
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
          DialogResult rep=MessageBox.Show("Are you sure", "verification", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(rep==DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
