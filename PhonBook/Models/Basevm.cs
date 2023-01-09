using System.Text.RegularExpressions;
namespace PhonBook.Models;
public class Basemodel{
    public Basemodel()
    {
        Groups=new List<PhonBook.Core.Domain.Grops.Grop>();
        VmGrops=new List<VmGrop>();
        Vmcontacts= new List<Vmcontact>();
        vmGrop=new VmGrop();
        vmcontact=new Vmcontact();
    }
    public List<VmGrop> VmGrops { get; set; }
    public List<Vmcontact> Vmcontacts { get; set; }
    public List<PhonBook.Core.Domain.Grops.Grop> Groups { get; set; }
    public VmGrop vmGrop { get; set; }
    public Vmcontact vmcontact { get; set; }
    public string Status_Page { get; set; }
}