using SaceShips.Lib.Interfaces;
namespace SaceShips.Lib.Classes;
using System.Collections.Generic;

public class Tree
{
    private Func<object, object> func_of_check;
    public Dictionary<object, object> first_layer;
    public Tree(Func<object, object> f_decision){
        func_of_check = f_decision;
    }
    void teach(List<List<object>> featers, List<object> ansers){
        var x = Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_of_check);
        int counter;
        int stage = 0;
        object flag;
        foreach (var item in featers)
        {
            first_layer.TryAdd(func_of_check(item[0]), (object)Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_of_check));
            first_layer.TryGetValue(func_of_check(item[0]), out flag);
            counter = 0; 
            foreach (var subitem in item.GroupBy(x => counter == 0 & counter++ != item.Count))
            {
                ((TreeNode)flag).nexts.TryAdd(func_of_check(item[0]), (object)Hwdtech.IoC.Resolve<TreeNode>("SpaceShip.Lib.Get.Node", func_of_check));
                ((TreeNode)flag).nexts.TryGetValue(func_of_check(item[0]), out flag);
            }
            ((TreeNode)flag).nexts.Add(func_of_check(item[0]), (object)ansers[stage]);
            stage++;
        }
    }
}
