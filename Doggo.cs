using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlExample;
public class Doggo
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public byte[]? Photo { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public ICollection<DoggoInfo> DoggoInfo { get; set; }
}
