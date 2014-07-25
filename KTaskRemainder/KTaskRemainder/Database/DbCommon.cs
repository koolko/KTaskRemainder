using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KTaskRemainder.Database
{
    public class DbCommon
    {
        public const string TABLE_NAME = "Tasks";
        public const string GUIN_COLUMN = "Guid";
        public const string TEXT_COLUMN = "Text";
        public const string IMPORTANT_COLUMN = "Important";
        public const string URGENT_COLUMN = "Urgent";

        public class Task
        {
            private Guid _guid;
            private string _text;
            private bool _important;
            private bool _urgent;

            public Guid Guid
            {
                get { return _guid; }
            }

            public string Text
            {
                get { return _text; }
            }

            public bool Important
            {
                get { return _important; }
            }

            public bool Urgent
            {
                get { return _urgent; }
            }

            public Task(string guid, string text, string important, string urgent)
            {
                if (Guid.TryParse(guid, out _guid))
                {
                    _text = text;
                    _important = important == "1" ? true : false;
                    _urgent = urgent == "1" ? true : false;
                }
            }
        }
    }
}
