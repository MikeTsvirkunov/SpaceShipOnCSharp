namespace SaceShips.Lib;


public class FrontMove: Movement
{
    private InterfaceObject moveable_obj;

    public FrontMove(InterfaceObject o){
        this.moveable_obj = o;
    }

    public void move(int t=1) {
        if (this.moveable_obj.ParamExist("coord"))
        {
            if (this.moveable_obj.ParamExist("speed")){
                this.moveable_obj.SetParam("coord", new double[2] { this.moveable_obj.GetParam("coord")[0] + t * this.moveable_obj.GetParam("speed")[0],
                                                                    this.moveable_obj.GetParam("coord")[1] + t * this.moveable_obj.GetParam("speed")[1] });
            }
            else throw new ArgumentException("Object without speed");
        }
        else throw new ArgumentException("Object without coord");
    }
}


public class RotationMove: Movement
{
    private UIObject moveable_obj;

    public RotationMove(UIObject o){
        this.moveable_obj = o;
    }

    public void move(int t=1) {
        if (this.moveable_obj.ParamExist("angle"))
        {
            if (this.moveable_obj.ParamExist("angle_speed")){
                this.moveable_obj.SetParam("angle", this.moveable_obj.GetParam("angle") + this.moveable_obj.GetParam("angle_speed") * t);
            }
            else throw new ArgumentException("Object without angle_speed");
        }
        else throw new ArgumentException("Object without angle");
    }
}


// eval "$(ssh-agent -s)"
// ssh-add ../sshgit
