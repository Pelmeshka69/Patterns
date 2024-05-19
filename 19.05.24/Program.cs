using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _19._05._24
{
    public interface IMediaPlayer
    {
        void PlayAudio(string fileName);
        void PlayVideo(string fileName);
    }

    public class MediaPlayer : IMediaPlayer
    {
        public void PlayAudio(string fileName)
        {
            Console.WriteLine("Playing audio file " + fileName);
        }

        public void PlayVideo(string fileName)
        {
            Console.WriteLine("Playing video file " + fileName);
        }
    }

    public class LegacyPlayer
    {
        public void PlayOldAudio(string fileName)
        {
            Console.WriteLine("Playing old audio file " + fileName);
        }
    }

    public class AudioAdapter : IMediaPlayer
    {
        private LegacyPlayer legacyPlayer;

        public AudioAdapter(LegacyPlayer legacyPlayer)
        {
            this.legacyPlayer = legacyPlayer;
        }

        public void PlayAudio(string fileName)
        {
            legacyPlayer.PlayOldAudio(fileName);
        }

        public void PlayVideo(string fileName)
        {
            throw new NotSupportedException();
        }
    }

    public abstract class MediaControl
    {
        protected IMediaDevice device;

        protected MediaControl(IMediaDevice device)
        {
            this.device = device;
        }

        public abstract void Play();
        public abstract void Pause();
        public abstract void Stop();
    }

    public class VideoMedia : MediaControl
    {
        public VideoMedia(IMediaDevice device) : base(device) { }

        public override void Play()
        {
            device.PowerOn();
            Console.WriteLine("Playing video...");
        }

        public override void Pause()
        {
            Console.WriteLine("Video paused.");
        }

        public override void Stop()
        {
            Console.WriteLine("Stopping video...");
            device.PowerOff();
        }
    }

    public class AudioMedia : MediaControl
    {
        public AudioMedia(IMediaDevice device) : base(device) { }

        public override void Play()
        {
            device.PowerOn();
            Console.WriteLine("Playing audio...");
        }

        public override void Pause()
        {
            Console.WriteLine("Audio paused.");
        }

        public override void Stop()
        {
            Console.WriteLine("Stopping audio...");
            device.PowerOff();
        }
    }

    public interface IMediaDevice
    {
        void PowerOn();
        void PowerOff();
    }

    public class TV : IMediaDevice
    {
        public void PowerOn()
        {
            Console.WriteLine("TV is on.");
        }

        public void PowerOff()
        {
            Console.WriteLine("TV is off.");
        }
    }

    public class Speakers : IMediaDevice
    {
        public void PowerOn()
        {
            Console.WriteLine("Speakers are on.");
        }

        public void PowerOff()
        {
            Console.WriteLine("Speakers are off.");
        }
    }

    public class Projector : IMediaDevice
    {
        public void PowerOn()
        {
            Console.WriteLine("Projector is on.");
        }

        public void PowerOff()
        {
            Console.WriteLine("Projector is off.");
        }
    }

    public class HomeTheaterFacade
    {
        private IMediaPlayer mediaPlayer;
        private IMediaDevice mediaDevice;

        public HomeTheaterFacade(IMediaPlayer mediaPlayer, IMediaDevice mediaDevice)
        {
            this.mediaPlayer = mediaPlayer;
            this.mediaDevice = mediaDevice;
        }

        public void WatchMovie(string movieName)
        {
            mediaDevice.PowerOn();
            mediaPlayer.PlayVideo(movieName);
        }

        public void StopMovie()
        {
            mediaDevice.PowerOff();
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            IMediaPlayer mediaPlayer = new MediaPlayer();
            LegacyPlayer legacyPlayer = new LegacyPlayer();
            IMediaPlayer audioAdapter = new AudioAdapter(legacyPlayer);
            IMediaDevice tv = new TV();
            IMediaDevice speakers = new Speakers();
            MediaControl videoMedia = new VideoMedia(tv);
            MediaControl audioMedia = new AudioMedia(speakers);
            HomeTheaterFacade homeTheater = new HomeTheaterFacade(mediaPlayer, tv);

            string fileName = "test.mp3";
            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("File name cannot be null or empty.");
                return;
            }

            while (true)
            {
                Console.WriteLine("1. Watch a movie");
                Console.WriteLine("2. Play an audio file");
                Console.WriteLine("3. Pause");
                Console.WriteLine("4. Stop");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            homeTheater.WatchMovie("test.mp4");
                            break;
                        case "2":
                            audioAdapter.PlayAudio(fileName);
                            break;
                        case "3":
                            videoMedia.Pause();
                            audioMedia.Pause();
                            break;
                        case "4":
                            videoMedia.Stop();
                            audioMedia.Stop();
                            homeTheater.StopMovie();
                            break;
                        case "5":
                            return;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    }
}