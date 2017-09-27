using System;
using Foundation;
using UIKit;

namespace Discrectionary
{
    public partial class ViewController : UIViewController, INSUrlSessionDownloadDelegate, INSUrlSessionDelegate
    {
        private NSUrlSession session;
        private NSUrlSessionDownloadTask downloadTask;

		protected ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (session == null)
            {
                var config = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("test");
                config.SessionSendsLaunchEvents = true;
                config.Discretionary = true;

                session = NSUrlSession.FromConfiguration(config, this, null);
            }

			downloadTask = session.CreateDownloadTask(new NSUrl("https://rss.art19.com/episodes/439bd7ab-177b-4552-a102-eca19b7e50fd.mp3"));

            downloadTask.Resume();
        }

        [Export("URLSession:task:didCompleteWithError:")]
        public void DidCompleteWithError(NSUrlSession session, NSUrlSessionTask task, NSError error)
        {
            if (error != null)
            {
                Console.WriteLine("Error Downloading: " + error.Description);
            }
        }

        public void DidFinishDownloading(NSUrlSession session, NSUrlSessionDownloadTask downloadTask, NSUrl location)
        {
            Console.WriteLine("Downloaded to location " + location);
		}
    }
}
