namespace SaceShips.Lib;

public class FrontMove: Movement
{
    public void action(dynamic o, int t = 1) {
        if (o is UIObject)
        {
            if (o.exist_param("speed") & o.exist_param("coord"))
            {
                o.set_param("coord", new double[2] { o.get_param("coord")[0] + t * o.get_param("speed")[0],
                                                     o.get_param("coord")[1] + t * o.get_param("speed")[1] });
            }
            else
            {
                if (o.exist_param("speed")) throw new ArgumentException("Object without coord");
                else throw new ArgumentException("Object without speed");
            }
        }
        else throw new ArgumentException("Incorect move object");
    }
}

public class RotationMove : Movement
{
    public void action(dynamic o, int t = 1)
    {
        if (o is UIObject)
        {
            if (o.exist_param("angle_speed") & o.exist_param("angle"))
            {
                dynamic angle_speed = o.get_param("angle_speed");
                o.set_param("angle", o.get_param("angle") + angle_speed * t);
                double[] speed = o.get_param("speed");
                o.set_param("speed", new double[2] { (speed[0] * Math.Cos(Math.PI * angle_speed / 180) - speed[1] * Math.Sin(Math.PI * angle_speed / 180)), 
                                                      speed[1] * Math.Cos(Math.PI * angle_speed / 180) - speed[0] * Math.Sin(Math.PI * angle_speed / 180) });
            }
            else
            {
                if (o.exist_param("angle_speed")) throw new ArgumentException("Object without angle");
                else throw new ArgumentException("Object without angle_speed");
            }
        }
        else throw new ArgumentException("Incorect move object");
    }
}