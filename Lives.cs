using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Lives
{
    private Window _gameWindow;
    private int _life = 10;
    private int X { get; set;}
    private int Y { get; set;}
    public int Life 
    { 
        get { return _life;}
        set { _life = value;}
    }
    public Lives(Window gameWindow)
    {
        X = 670;
        Y = 570;
        _gameWindow = gameWindow;
    }
    public void Draw()
    {
        int windowWidth = _gameWindow.Width;
        _gameWindow.FillRectangle(Color.RGBAColor(10, 29, 49, 100), 0, 555, windowWidth, 50);
    }
}