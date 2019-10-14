using System;
using System.Collections.Generic;
using SplashKitSDK;

public class Score
{
    private Window _gameWindow;
    private Timer _score;
    
    public Score(Window gameWindow,Timer Score)
    {
        _gameWindow = gameWindow;
        _score = Score;
    }
    public void Draw()
    {
        _gameWindow.DrawText("Score: " + Convert.ToString(_score.Ticks / 1000), Color.Black, "StencilStd.otf", 20, 10, 570);
    }
}