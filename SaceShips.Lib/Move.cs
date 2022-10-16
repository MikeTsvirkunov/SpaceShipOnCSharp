namespace SaceShips.Lib;


public class FrontMove : ICommand
{
    private InterfaceObject moveable_obj;

    public FrontMove(InterfaceObject o)
    {
        this.moveable_obj = o;
    }

    public void action()
    {
        if (this.moveable_obj.ParamExist("coord"))
        {
            if (this.moveable_obj.ParamExist("speed"))
            {
                this.moveable_obj.SetParam("coord", new dynamic[2] { this.moveable_obj.GetParam("coord")[0] + this.moveable_obj.GetParam("speed")[0],
                                                                    this.moveable_obj.GetParam("coord")[1] + this.moveable_obj.GetParam("speed")[1] });
            }
            else throw new ArgumentException("Object without speed");
        }
        else throw new ArgumentException("Object without coord");
    }
}


public class RotationMove : ICommand
{
    private UIObject moveable_obj;

    public RotationMove(UIObject o)
    {
        this.moveable_obj = o;
    }

    public void action()
    {
        if (this.moveable_obj.ParamExist("angle"))
        {
            if (this.moveable_obj.ParamExist("angle_speed"))
            {
                this.moveable_obj.SetParam("angle", this.moveable_obj.GetParam("angle") + this.moveable_obj.GetParam("angle_speed"));
                double a = this.moveable_obj.GetParam("angle_speed");
                double x = this.moveable_obj.GetParam("coord")[0] * Math.Cos(a)  - this.moveable_obj.GetParam("coord")[1] * Math.Sin(a);
                double y = this.moveable_obj.GetParam("coord")[0] * Math.Sin(a)  + this.moveable_obj.GetParam("coord")[1] * Math.Cos(a);
                
                this.moveable_obj.SetParam("coord", new double[2] {x, y});
            }
            else throw new ArgumentException("Object without angle_speed");
        }
        else throw new ArgumentException("Object without angle");
    }
}


// eval "$(ssh-agent -s)"
// ssh-add ../sshgit
