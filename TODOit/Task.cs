using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODOit
{
    class Task
    {
        private int id;
        private string text;
        private bool isCompleted;
        public int Id { get => Id; set { id = value; } }
        public string Text { get => text; set { text = value; } }
        public bool IsCompleted { get => isCompleted; set { isCompleted = value; } }
        public Task()
        {

        }
    }
}
