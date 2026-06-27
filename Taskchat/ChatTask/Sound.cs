using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace ChatTask
{
    public class Sound
    {
        public void PlaySound()
        {
            try
            {
                SoundPlayer player = new SoundPlayer("C:\\TaskChat\\Taskchat\\ChatTask\\WAVfil.wav");
                player.Play();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured when we played the song " + ex.Message);
            }
        }

    }
}

