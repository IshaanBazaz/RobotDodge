using System;
using SplashKitSDK;

public class Bullet
    {
        //variable declaration
        private Bitmap _bulletBitmap;
        private double _x, _y, _angle;
        private bool _active = false;
        private Vector2D Velocity { get; set;}
        public Bullet(double x, double y, double angle) //constructor to initialize the variables
        {
            _bulletBitmap = new Bitmap("Bullet","bullet.png");
            _x = x - _bulletBitmap.Width / 2;
            _y = y - _bulletBitmap.Height / 2;
            _angle = angle;
            _active = true;
        }

        public Bullet() //default constructor to initially set bullet to false
        {
            _active = false;
        }

        public void Shoot() //procedure to for handling when the bullet is shot
        {
            const int TOAST = 8;
            Vector2D movement = new Vector2D();
            Matrix2D rotation = SplashKit.RotationMatrix(_angle);
            Point2D fromPt = new Point2D() //getting a point for the robot
            {
                X =_x, Y = _y
            };
            Point2D toPt = new Point2D() //getting a point for the player
            {
                X = SplashKit.MousePosition().X,
                Y = SplashKit.MousePosition().Y
            };
            movement = SplashKit.UnitVector(SplashKit.VectorPointToPoint(fromPt, toPt));
            //movement.X += TOAST;
            Velocity = SplashKit.VectorMultiply(movement, TOAST);
            movement = SplashKit.MatrixMultiply(rotation, Velocity);
            _x += movement.X;
            _y += movement.Y;
            if ((_x > SplashKit.ScreenWidth() || _x < 0) || (_y > SplashKit.ScreenHeight()) || (_y < 0))
            {
                _active = false;
            }
        }

        public void Draw() //procedure to draw the bullet bitmap
        {
            if (_active) //ensuring the bullet is drawn only when it is active which is when the player shoots it
            {
                DrawingOptions options = SplashKit.OptionRotateBmp(_angle);
                _bulletBitmap.Draw(_x, _y, options);
            }
        }
        public bool CollidedWith(Robot other) //procedure to check whether the player collided with the robot
        {
            if (_active)
            {
                return _bulletBitmap.CircleCollision(_x, _y, other.CollisionCircle);
            }
            else
            {
                return false;
            }
        }
    }