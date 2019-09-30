using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDB
{
    public class Voting
    {
        [Key]
        public int Id { get; set; }
        public int Bad { get; set; }
        public int Normal { get; set; }
        public int Good { get; set; }
        public int Count { get; set; }
    }
}
