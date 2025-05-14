using System;
using System.IO;
using System.Media;

namespace Cybersecurity_Awareness_Bot_PART1
{
    public class voice_greeting
    {
        //constructor
        public voice_greeting()
        {
            //getting the app full location
            string full_location = AppDomain.CurrentDomain.BaseDirectory;

            //replace bin\\Debug\
            string new_path = full_location.Replace("bin\\Debug\\", "Cybersecurity.wav");
           
            //combining the paths
            string path_full = Path.Combine(new_path, "");

            audio(new_path);
         }//end of constructor

        //method for the welcome greeting audio
        private void audio(string new_path)
        {
            
                //start of try and catch loop
                try
                {
                //play audio
                using (SoundPlayer player = new SoundPlayer(new_path))
                {
                    player.PlaySync(); //Use Play() for asynchronous play
                }//end of using()
                }
                catch (Exception error)
                {
                    Console.WriteLine(error.Message);
                }
            }//end of try and catch loop
           
    }//end of class
}//end of namespace