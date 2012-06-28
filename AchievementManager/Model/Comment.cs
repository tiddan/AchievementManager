using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AchievementManager.Model
{
    public class Comment : ModelBase
    {
        //////////////////////////////////
        //
        // [VARIABLES]
        //
        //////////////////////////////////

        private DateTime _timestamp;
        private string _text;

        //////////////////////////////////
        //
        // [PROPERTIES]
        //
        //////////////////////////////////

        public DateTime TimeStamp
        {
            get
            {
                return _timestamp;
            }
            set
            {
                _timestamp = value;
                OnPropertyChanged("TimeStamp");
            }
        }

        public string TimestampText
        {
            get
            {
                return "Comment added : " + TimeStamp.ToShortDateString() + "  " + TimeStamp.ToShortTimeString();
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                OnPropertyChanged("Text");
            }
        }
    }
}
