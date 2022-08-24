using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExample;
public class DoggoInfo
{
    public int Id { get; set; }
    public string OwnerName { get; set; }
    public int Age { get; set; }
    public int DoggoId { get; set; }
    public Doggo Doggo { get; set; }
}
