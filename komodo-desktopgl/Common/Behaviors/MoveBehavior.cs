using Komodo.Core.ECS.Components;
using Komodo.Core.Engine.Input;
using Komodo.Lib.Math;

using GameTime = Microsoft.Xna.Framework.GameTime;

namespace Common.Behaviors
{
    public class MoveBehavior : BehaviorComponent
    {
        #region Constructors
        public MoveBehavior(int playerIndex) : base()
        {
            if (!InputManager.IsValidPlayerIndex(playerIndex))
            {
                playerIndex = 0;
            }
            PlayerIndex = playerIndex;
            SprintFactor = 2f;
            Velocity = 50f;
        }
        #endregion Constructors

        #region Members

        #region Public Members
        public int PlayerIndex { get; set; }
        public float SprintFactor { get; set; }
        public float Velocity { get; set; }
        #endregion Public Members

        #endregion Members

        #region Member Methods

        #region Public Member Methods
        public override void Update(GameTime gameTime)
        {
            var left = InputManager.GetInput("left", PlayerIndex);
            var right = InputManager.GetInput("right", PlayerIndex);
            var up = InputManager.GetInput("up", PlayerIndex);
            var down = InputManager.GetInput("down", PlayerIndex);
            var sprint = InputManager.GetInput("sprint", PlayerIndex);

            var quit = InputManager.GetInput("quit", PlayerIndex);

            var direction = Vector3.Zero;
            if (quit.State == InputState.Down)
            {
                Game.Exit();
            }
            if (left.State == InputState.Down)
            {
                direction += Vector3.Left;
            }
            if (right.State == InputState.Down)
            {
                direction += Vector3.Right;
            }
            if (up.State == InputState.Down)
            {
                direction += Vector3.Up;
            }
            if (down.State == InputState.Down)
            {
                direction += Vector3.Down;
            }
            if (sprint.State == InputState.Down)
            {
                direction *= SprintFactor;
            }

            Parent.Position += (
                direction
                * Velocity
                * (float)gameTime.ElapsedGameTime.TotalSeconds
            );
        }
        #endregion Public Member Methods

        #endregion Member Methods
    }
}