using System;
using SplashKitSDK;

public class Player
{
    //variable declaration
    private Bitmap _PlayerBitmap;
    public double X { get; private set; } //variable declaration for x value of bitmap using auto property
    public double Y { get; private set; }  //variable declaration for y value of bitmap using auto property
    public bool Quit { get; private set; } //variable declaration for quiting the game using auto property
    public bool Hit { get; private set; }
    public Bullet _bullet = new Bullet();

    private double _angle;
    public int Width 
    {
        get
        {
            return _PlayerBitmap.Width; //getting the width of the bitmap image
        }
    }
    public int Height 
    {
        get
        {
            return _PlayerBitmap.Height; //getting the height of the bitmap image
        }
    }
    public double Angle
    {
        get { return _angle; }
        set { _angle = value; }
    }

    public Player(Window gameWindow) //constructor
    {
        Angle = 0;
        _PlayerBitmap = new Bitmap("player", "Player.png"); //creating a new bitmap object with the image in the resources
        //setting x and y value of the bitmap to make it appear on the centre of the screen
        X = (gameWindow.Width - Width) / 2;
        Y = (gameWindow.Height - Height) / 2;
        Quit = false; //setting the Quit value to false by default ensuring game runs unless user wants to quit
    }
    public void Shoot()
        {
            Matrix2D anchorMatrix = SplashKit.TranslationMatrix(SplashKit.PointAt(_PlayerBitmap.Width / 2, _PlayerBitmap.Height / 2));

            // Move centre point of picture to origin
            Matrix2D result = SplashKit.MatrixMultiply(SplashKit.IdentityMatrix(), SplashKit.MatrixInverse(anchorMatrix));
            // Rotate around origin
            result = SplashKit.MatrixMultiply(result, SplashKit.RotationMatrix(_angle));
            // Move it back...
            result = SplashKit.MatrixMultiply(result, anchorMatrix);

            // Now move to location on screen...
            result = SplashKit.MatrixMultiply(result, SplashKit.TranslationMatrix(X, Y));

            // Result can now transform a point to the player's location
            // Get right/centre
            Vector2D vector = new Vector2D();
            vector.X = _PlayerBitmap.Width;
            vector.Y = _PlayerBitmap.Height / 2;
            // Transform it...
            vector = SplashKit.MatrixMultiply(result, vector);
            _bullet = new Bullet(vector.X, vector.Y, Angle);
        }

        public void BulletShoot() //procedure to shoot the bullet
        {
            _bullet.Shoot();
        }

    public void Draw() //drawing the bitmap on the window
    {
        _PlayerBitmap.Draw(X, Y);
        _bullet.Draw();
    }

    public void HandleInput()
    {
        int speed = 5;

        //handling boost using a cheat key combination
        if(SplashKit.KeyDown(KeyCode.LeftShiftKey) && SplashKit.KeyDown(KeyCode.LeftCtrlKey) && SplashKit.KeyDown(KeyCode.BKey))
        {
            speed = 20; //increasing the speed of movement
        }

        //handling up movement when user presses up arrow key or W key
        if(SplashKit.KeyDown(KeyCode.UpKey) || SplashKit.KeyDown(KeyCode.WKey))
        {
            Y = Y - speed;
        }

        //handling down movement when user presses down arrow key or S key
        if(SplashKit.KeyDown(KeyCode.DownKey) || SplashKit.KeyDown(KeyCode.SKey))
        {
            Y = Y + speed;
        }

        //handling right movement when user presses right arrow key or D key
        if(SplashKit.KeyDown(KeyCode.RightKey) || SplashKit.KeyDown(KeyCode.DKey))
        {
            X = X + speed;
        }

        //handling left movement when user presses left arrow key or A key
        if(SplashKit.KeyDown(KeyCode.LeftKey) || SplashKit.KeyDown(KeyCode.AKey))
        {
            X = X - speed;
        }
        //handling bullets
        if (SplashKit.MouseClicked(MouseButton.LeftButton))
        {
            Shoot();
        }
        BulletShoot();
        //handling user wish to quit when he presses ESC key
        if(SplashKit.KeyDown(KeyCode.EscapeKey))
        {
            Quit = true;
        }
    }

    //procedure for ensuring the bitmap stays on screen by wrapping it around the screen
    public void StayOnWindow(Window limit)
    {
        const int GAP = 10;
        if ( X < GAP ) X = limit.Width - GAP;
        if ( X > (limit.Width - GAP) ) X = GAP;
        if ( Y < GAP) Y = limit.Height - GAP;
        if ( Y > (limit.Height - GAP) ) Y = GAP;
    }

    public bool CollidedWith(Robot other) //procedure to check whether the player collided with the robot
    {
        return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
    }

}
