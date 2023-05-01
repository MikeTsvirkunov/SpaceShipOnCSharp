using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;

#nullable enable
public class Tree
{
    private Func<object, object> func_of_check;
    public Dictionary<object, object> first_layer;
    public Tree(Func<object, object> f_decision){
        func_of_check = f_decision;
        first_layer = new Dictionary<object, object>();
    }
    public void teach(List<List<object>> featers, List<object> ansers){
        var x = Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_of_check);
        int stage = 0;
        object flag;
        foreach (var item in featers)
        {
            first_layer.TryAdd(func_of_check(item[0]), (object)Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_of_check));
            first_layer.TryGetValue(func_of_check(item[0]), out flag);
            foreach (var subitem in item.GetRange(1, item.Count-2))
            {
                ((TreeNode)flag).nexts.TryAdd(func_of_check(subitem), (object)Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_of_check));
                ((TreeNode)flag).nexts.TryGetValue(func_of_check(subitem), out flag);
            }
            ((TreeNode)flag).nexts.Add(func_of_check(item[item.Count-1]), (object)ansers[stage]);
            stage++;
        }
    }

    public object? get_solution(List<object> obj)
    {
        object flag;
        first_layer.TryGetValue(func_of_check(obj[0]), out flag);
        if (flag == null) return null;
        foreach (var item in obj.GetRange(1, obj.Count-1))
        {
            ((TreeNode)flag).nexts.TryGetValue(func_of_check(item), out flag);
            if (flag == null) return flag;
        }
        return flag;
    }
}
