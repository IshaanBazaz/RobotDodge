using System;
using SplashKitSDK;

public abstract class Robot
{
    //variable declaration
    public double X { get; private set; } //variable declaration for x value of bitmap using auto property
    public double Y { get; private set; }  //variable declaration for y value of bitmap using auto property
    public Color MainColor { get; private set; }
    private Vector2D Velocity { get; set;} //creating a vector to make the robot move
    public int Width 
    {
        get
        {
            return 50; //getting the width of the bitmap image
        }
    }
    public int Height 
    {
        get
        {
            return 50; //getting the height of the bitmap image
        }
    }
    public Circle CollisionCircle
    {
        get
        {
            return SplashKit.CircleAt(X + (Width/2), Y + (Width/2), 20); //creating a circle boundary around the robot 
        }
    }

    public Robot(Window gameWindow, Player player) //constructor
    {
        MainColor = Color.RandomRGB(200); //asigning a random color for drawing the robot
        if (SplashKit.Rnd() < 0.5)
        {
            //making the robot appear from top or bottom of screen randomly

            X = SplashKit.Rnd(gameWindow.Width); //seting X value to any random position within the screen bounds
            if (SplashKit.Rnd() < 0.5)
                Y = -Height; //making the robot appear from top side of screen
            else
                Y = gameWindow.Height; //making the robot appear from bottom side of screen
        }
        else
        {
            //making the robot appear from top or botteom of screen randomly

            Y = SplashKit.Rnd(gameWindow.Height); //seting Y value to any random position within the screen bounds
            if (SplashKit.Rnd() < 0.5)
                X = -Width; //making the robot appear from left side of screen
            else
                X = gameWindow.Width; //making the robot appear from right side of screen
        }
        const int SPEED = 4;
        Point2D fromPt = new Point2D() //getting a point for the robot
        {
            X =X, Y = Y
        };
        Point2D toPt = new Point2D() //getting a point for the player
        {
            X =player.X, Y = player.Y
        };
        //calculating the direction for the robot to head
        Vector2D dir;
        dir = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
        //setting the speed and direction to assign it to the 'Velocity'
        Velocity = SplashKit.VectorMultiply(dir, SPEED);
    }

    public abstract void Draw(); //abstract method as a placeholder for overriding methods of children class

    //procedure to make the robot move by the amount in its velocity property
    public void Update()
    {
        X = X + Velocity.X;
        Y = Y + Velocity.Y;
    }

    //procedure to check whether the robot is off the screen
    public bool IsOffscreen(Window screen)
    {
        return (X < -Width || X > screen.Width || Y < -Height || Y > screen.Height);
    }
}

public class Boxy : Robot
{
    public Boxy(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        double leftX, rightX, eyeY, mouthY;
        leftX = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        SplashKit.DrawRectangle(Color.Gray, X , Y, 50, 50);
        SplashKit.DrawRectangle(MainColor, leftX, eyeY, 10, 10);
        SplashKit.DrawRectangle(MainColor, rightX, eyeY, 10, 10);
        SplashKit.DrawRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.DrawRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }
}

public class Roundy : Robot
{
    public Roundy(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        double leftX, midX, rightX;
        double eyeY, midY, mouthY;
        leftX = X + 17;
        midX = X + 25;
        rightX = X + 33;
        eyeY = Y + 20;
        midY = Y + 25;
        mouthY = Y + 35;
        SplashKit.FillCircle(Color.White, midX, midY, 25);
        SplashKit.DrawCircle(Color.Gray, midX, midY, 25);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 5);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 5);
        SplashKit.FillEllipse(Color.Gray, X, eyeY, 50, 30);
        SplashKit.DrawLine(Color.Black, X, mouthY, X + 50, Y + 35);
    }
}
public class BoxyAndRoundy : Robot
{
    public BoxyAndRoundy(Window gameWindow, Player player) : base(gameWindow, player)//constructor
    {
    }
    public override void Draw() //drawing the robot bitmap on the window
    {
        double leftX, rightX, eyeY, mouthY;
        leftX = X + 12;
        rightX = X + 27;
        eyeY = Y + 10;
        mouthY = Y + 30;
        SplashKit.FillRectangle(Color.White, X , Y, 50, 50);
        SplashKit.DrawRectangle(Color.Gray, X , Y, 50, 50);
        SplashKit.FillCircle(MainColor, leftX, eyeY, 7);
        SplashKit.FillCircle(MainColor, rightX, eyeY, 7);
        SplashKit.FillRectangle(MainColor, leftX, mouthY, 25, 10);
        SplashKit.FillRectangle(MainColor, leftX + 2, mouthY + 2, 21, 6);
    }
}