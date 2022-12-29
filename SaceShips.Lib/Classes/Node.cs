using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;


public class TreeNode: ISource
{
    private Func<object, object> func_of_check;
    public Dictionary<object, object?> nexts {set; get;}
    public TreeNode(Func<object, object> f_decision, Dictionary<object, object>? n = null)
    {
        nexts = n ?? new Dictionary<object, object>();
        func_of_check = f_decision;
    }
    public object? step_forward(object arg){
        nexts.TryGetValue(func_of_check(arg), out var x);
        return x;
    }
}
