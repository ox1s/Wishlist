using System;
using System.Collections.Generic;

class ItemManager
{
    private IItemManagerStrategy _itemManagerStrategy;

    public void SetDisplayStrategy(IItemManagerStrategy strategy)
    {
        _itemManagerStrategy = strategy;
    }

    public void DisplayItem(IItem item)
    {
        _itemManagerStrategy?.Display(item);
    }

    public void DisplayAllItems(List<IItem> items)
    {
        _itemManagerStrategy?.DisplayAllItems(items);
    }
    
    public void SwitchDisplayStrategy(string strategyType)
    {
        if (strategyType == "simple")
        {
            SetDisplayStrategy(new SimpleDisplayStrategy());
        }
        else if (strategyType == "detailed")
        {
            SetDisplayStrategy(new DetailedDisplayStrategy());
        }
        else
        {
            Console.WriteLine("Invalid strategy type. Defaulting to SimpleDisplayStrategy.");
            SetDisplayStrategy(new SimpleDisplayStrategy());
        }
    }
}
