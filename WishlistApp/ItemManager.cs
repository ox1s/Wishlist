class ItemManager
{
    private IItemManagerStrategy _itemManagerStrategy;

    public ItemManager() { }
    public ItemManager(IItemManagerStrategy strategy)
    {
        this._itemManagerStrategy = strategy;
    }

    public void SetDisplayStrategy(IItemManagerStrategy strategy)
    {
        this._itemManagerStrategy = strategy;
    }

    public void DisplayItem(IItem item)
    {
        this._itemManagerStrategy.Display(item);
    }

    public void DisplayAllItems(List<IItem> items)
    {
        if (_itemManagerStrategy == null)
        {
            Console.WriteLine("Display strategy is not set.");
            return;
        }
        _itemManagerStrategy.DisplayAllItems(items);
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
        else if (strategyType == "price")
        {
            SetDisplayStrategy(new PriceDisplayStrategy());
        }
        else if (strategyType == "input")
        {
            SetDisplayStrategy(new DisplayStrategyWithoutPurсhase());
        }
        else
        {
            Console.WriteLine("Invalid strategy type. Defaulting to SimpleDisplayStrategy.");
            SetDisplayStrategy(new SimpleDisplayStrategy());
        }
    }
}
