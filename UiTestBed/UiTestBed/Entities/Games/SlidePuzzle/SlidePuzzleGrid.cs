using System;
using System.Collections.Generic;
using System.Text;
using FlatRedBall;
using FlatRedBall.Input;
using FlatRedBall.AI.Pathfinding;
using FlatRedBall.Graphics.Animation;
using FlatRedBall.Graphics.Particle;

#if FRB_XNA || SILVERLIGHT
using FlatRedBall.Instructions;
using FrbUi;
using FrbUi.Controls;
using Keys = Microsoft.Xna.Framework.Input.Keys;
using Vector3 = Microsoft.Xna.Framework.Vector3;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;
#endif

using FlatRedBall.Math.Geometry;
using FlatRedBall.Math.Splines;
using BitmapFont = FlatRedBall.Graphics.BitmapFont;
using Cursor = FlatRedBall.Gui.Cursor;
using GuiManager = FlatRedBall.Gui.GuiManager;
using FrbUi.Layouts;

namespace UiTestBed.Entities.Games.SlidePuzzle
{
	public partial class SlidePuzzleGrid
	{
	    private GridLayout _grid;
	    private Dictionary<int, Button> _sliderButtons;
	    private double? _transitionStartTime;
	    private float _slideDestinationX, _slideDestinationY;
	    private float _slideStartX, _slideStartY;
	    private int _slideToRowIndex, _slideToColIndex;
	    private ILayoutable _slidingTile;

		private void CustomInitialize()
		{
		    _sliderButtons = new Dictionary<int, Button>();

		    _grid = UiControlManager.Instance.CreateControl<GridLayout>();
		    _grid.Spacing = 10;
		    _grid.Margin = 20;
		    _grid.BackgroundAnimationChains = GlobalContent.GameContent_Slider_GridBackground;
		    _grid.CurrentAnimationChainName = "Idle";

		    for (int x = 0; x < (GridSize*GridSize); x++)
		        CreateSliderButton(x);

            RandomizeSliderPlacement();
		}

	    private void CustomActivity()
		{
            if (_transitionStartTime != null)
            {
                var pct = (TimeManager.CurrentTime - _transitionStartTime.Value) / SlideSeconds;
                if (pct >= 1)
                {
                    _transitionStartTime = null;
                    _grid.AddItem(_slidingTile, _slideToRowIndex, _slideToColIndex);
                    CheckIfWon();
                }
                else
                {
                    _slidingTile.X = _slideStartX + ((_slideDestinationX - _slideStartX) * (float)pct);
                    _slidingTile.Y = _slideStartY + ((_slideDestinationY - _slideStartY) * (float)pct);
                }
            }
		}

		private void CustomDestroy()
		{


		}

        private static void CustomLoadStaticContent(string contentManagerName)
        {


        }

        private void CreateSliderButton(int index)
        {
            var btn = UiControlManager.Instance.CreateControl<Button>();
            btn.ScaleX = ButtonSize;
            btn.ScaleY = ButtonSize;
            
            // Don't texture the last button to make it the empty space
            var lastIndex = (GridSize * GridSize) - 1;
            if (index < lastIndex)
            {
                btn.AnimationChains = GlobalContent.GameContent_Slider_SliderButton;
                btn.StandardAnimationChainName = "Idle";
                btn.FocusedAnimationChainName = "Focused";
                btn.Text = (index + 1).ToString();
                btn.OnClicked = SlideTileClicked;
            }
            else
            {
                // Disable the placeholder square
                btn.Enabled = false;
                btn.Alpha = 0;
            }

            _sliderButtons.Add(index, btn);
        }

        private void RandomizeSliderPlacement()
        {
            // Randomize the start positions of all but the empty button
            var positions = new List<int>();
            var unpositionedButtons = new List<Button>();
            for (int x = 0; x < (GridSize * GridSize) - 1; x++)
            {
                positions.Add(x);
                unpositionedButtons.Add(_sliderButtons[x]);
            }

            var rand = new Random();
            while (positions.Count > 0)
            {
                var positionIndex = rand.Next(0, positions.Count);
                var buttonIndex = rand.Next(0, unpositionedButtons.Count);
                var index = positions[positionIndex];
                var btn = unpositionedButtons[buttonIndex];

                // Compute where to add this button to the grid
                var row = 0;
                var currentIndex = index;
                while (currentIndex >= GridSize)
                {
                    currentIndex -= GridSize;
                    row++;
                }

                _grid.AddItem(btn, row, currentIndex);

                positions.RemoveAt(positionIndex);
                unpositionedButtons.RemoveAt(buttonIndex);
            }

            // Now position the empty tile
            var emptyTile = _sliderButtons[(GridSize * GridSize) - 1];
            _grid.AddItem(emptyTile, GridSize - 1, GridSize - 1);
            emptyTile.Alpha = 0;
        }

        private void SlideTileClicked(ILayoutable sender)
        {
            if (_transitionStartTime != null || HasWon)
                return;

            int rowIndex;
            int columnIndex;
            if (!_grid.TryGetItemPosition(sender, out rowIndex, out columnIndex))
                throw new InvalidOperationException("Item is not part of the grid");

            var emptyTile = _sliderButtons[(GridSize * GridSize) - 1];
            var aboveTile = _grid.GetItemAt(rowIndex - 1, columnIndex);
            var belowTile = _grid.GetItemAt(rowIndex + 1, columnIndex);
            var rightTile = _grid.GetItemAt(rowIndex, columnIndex + 1);
            var leftTile = _grid.GetItemAt(rowIndex, columnIndex - 1);

            // If none of the adjacent tiles are the empty tile, do nothing
            if (aboveTile != emptyTile && belowTile != emptyTile && leftTile != emptyTile && rightTile != emptyTile)
                return;

            int emptyTileRowIndex;
            int emptyTileColIndex;
            _grid.TryGetItemPosition(emptyTile, out emptyTileRowIndex, out emptyTileColIndex);

            // store information to perform a smooth slide
            _transitionStartTime = TimeManager.CurrentTime;
            _slideDestinationX = emptyTile.X;
            _slideDestinationY = emptyTile.Y;
            _slideStartX = sender.X;
            _slideStartY = sender.Y;
            _slideToRowIndex = emptyTileRowIndex;
            _slideToColIndex = emptyTileColIndex;
            _slidingTile = sender;

            // Swap the tiles
            // Detach them and swap them
            _grid.RemoveItem(emptyTile);
            _grid.RemoveItem(sender);

            // Immediately put the empty tile in position
            _grid.AddItem(emptyTile, rowIndex, columnIndex);

            MoveCount++;
        }

        private void CheckIfWon()
        {
            int index = 0;
            for (int row = 0; row < GridSize; row++)
            {
                for (int col = 0; col < GridSize; col++)
                {
                    index++;
                    var item = _grid.GetItemAt(row, col) as Button;
                    if (item != _sliderButtons[index - 1])
                    {
                        HasWon = false;
                        return;
                    }
                }
            }

            // if we got here, all the tils are correctly positioned
            HasWon = true;
        }
	}
}
