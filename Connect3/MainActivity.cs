﻿using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;

namespace Connect3
{
    //https://developer.xamarin.com/guides/android/user_interface/material_theme/

    //TODO game is continued even after one player has won. prevent playing after linearLayout for game reset is visible
    [Activity(Label = "Connect3", MainLauncher = true, Icon = "@drawable/icon",
        Theme = "@android:style/Theme.DeviceDefault.Light")]
    public class MainActivity : Activity
    {
        private ImageView _image00;
        private ImageView _image01;
        private ImageView _image02;
        private ImageView _image10;
        private ImageView _image11;
        private ImageView _image12;
        private ImageView _image20;
        private ImageView _image21;
        private ImageView _image22;
        private LinearLayout _linearLayoutResetGame;
        private Button _buttonResetGame;
        private TextView _textViewMessage;
        private Player _activePlayer = Player.RedPlayer;
        
        private readonly BlockStatus[] _allBlocksStatus=new BlockStatus[9];
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView (Resource.Layout.Main);
            initFields();
        }

        private void initFields()
        {
            _image00 = FindViewById<ImageView>(Resource.Id.imageView00);
            _image00.Click += imamge_Click;
            _image01 = FindViewById<ImageView>(Resource.Id.imageView01);
            _image01.Click += imamge_Click;
            _image02 = FindViewById<ImageView>(Resource.Id.imageView02);
            _image02.Click += imamge_Click;
            _image10 = FindViewById<ImageView>(Resource.Id.imageView10);
            _image10.Click += imamge_Click;
            _image11 = FindViewById<ImageView>(Resource.Id.imageView11);
            _image11.Click += imamge_Click;
            _image12 = FindViewById<ImageView>(Resource.Id.imageView12);
            _image12.Click += imamge_Click;
            _image20 = FindViewById<ImageView>(Resource.Id.imageView20);
            _image20.Click += imamge_Click;
            _image21 = FindViewById<ImageView>(Resource.Id.imageView21);
            _image21.Click += imamge_Click;
            _image22 = FindViewById<ImageView>(Resource.Id.imageView22);
            _image22.Click += imamge_Click;

            _linearLayoutResetGame = FindViewById<LinearLayout>(Resource.Id.linearLayoutResetGame);
            _linearLayoutResetGame.Visibility=ViewStates.Invisible;

            _textViewMessage = FindViewById<TextView>(Resource.Id.textViewMessage);
            _buttonResetGame = FindViewById<Button>(Resource.Id.buttonReset);
            _buttonResetGame.Click += _buttonResetGame_Click;
        }

        private void _buttonResetGame_Click(object sender, System.EventArgs e)
        {
            _linearLayoutResetGame.Visibility=ViewStates.Invisible;
            resetGame();
        }

        private void imamge_Click(object sender, System.EventArgs e)
        {
            ImageView imageView = (ImageView) sender;
            int clickedBlock = int.Parse(imageView.Tag.ToString());
            if (_allBlocksStatus[clickedBlock]!=BlockStatus.UnTaken)
            {
                showBlockAlreadyTaken();
                return;
            }
            saveCurrentlyClickedBlockStatus(clickedBlock);
            animateImageIntoField(imageView);
            toggleActivePlayer();
            bool onePlayerHasWon=checkWinner();
            if(onePlayerHasWon)
                return;
            checkAllBlocksTaken();
        }

        private void checkAllBlocksTaken()
        {
            foreach (BlockStatus blockStatus in _allBlocksStatus)
            {
                if(blockStatus==BlockStatus.UnTaken)
                    return;
            }
            _textViewMessage.Text = "All Blocks Are Taken. No Winner";
            _linearLayoutResetGame.Visibility=ViewStates.Visible;
        }

        private bool checkWinner()
        {
            Winner winner;
            //row win
            winner= checkAllSame(0, 1, 2);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            winner = checkAllSame(3,4,5);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            winner = checkAllSame(6,7,8);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            //column win
            winner = checkAllSame(0,3,6);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            winner = checkAllSame(1,4,7);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            winner = checkAllSame(2,5,8);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            //diagonal win
            winner = checkAllSame(0, 4,8);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            winner = checkAllSame(2,4,6);
            if (winner != Winner.Non)
            {
                showWinner(winner);
                return true;
            }
            return false;
        }


        private void showWinner(Winner winner)
        {
            string message = "";
            if (winner == Winner.RedPlayer)
                message = "Red Player Won";
            else if (winner == Winner.YellowPlayer)
                message = "Yellow Player Won";
            _textViewMessage.Text = message;
            _linearLayoutResetGame.Visibility=ViewStates.Visible;
        }

        private void resetGame()
        {
            for(int i=0;i<_allBlocksStatus.Length;i++)
                _allBlocksStatus[i]=BlockStatus.UnTaken;
            _image00.SetImageResource(0);
            _image01.SetImageResource(0);
            _image02.SetImageResource(0);
            _image10.SetImageResource(0);
            _image11.SetImageResource(0);
            _image12.SetImageResource(0);
            _image20.SetImageResource(0);
            _image21.SetImageResource(0);
            _image22.SetImageResource(0);
        }

        private Winner checkAllSame(int i, int j, int k)
        {
            //check for red
            if(_allBlocksStatus[i]==BlockStatus.Red && _allBlocksStatus[j] == BlockStatus.Red && _allBlocksStatus[k] == BlockStatus.Red)
                return Winner.RedPlayer;
            if(_allBlocksStatus[i] == BlockStatus.Yellow && _allBlocksStatus[j] == BlockStatus.Yellow && _allBlocksStatus[k] == BlockStatus.Yellow)
                return Winner.YellowPlayer;
            //check for yellow
            return Winner.Non;
        }


        private void showBlockAlreadyTaken()
        {
            Toast.MakeText(this, "already taken", ToastLength.Short).Show();
        }

        private void animateImageIntoField(ImageView imageView)
        {
            imageView.TranslationY = -1000f;
            imageView.SetImageResource(getImageResourceBasedOnActivePlayer());
            imageView.Animate().TranslationYBy(1000f).Rotation(360).SetDuration(500);
        }

        private void saveCurrentlyClickedBlockStatus(int clickedBlock)
        {
            switch (_activePlayer)
            {
                case Player.RedPlayer:
                    _allBlocksStatus[clickedBlock]=BlockStatus.Red;
                    break;
                    case Player.YellowPlayer:
                    _allBlocksStatus[clickedBlock]=BlockStatus.Yellow;
                    break;
            }
        }

        private int getImageResourceBasedOnActivePlayer()
        {
            int imageResource=0;
            switch (_activePlayer)
            {
                case Player.RedPlayer:
                    imageResource = Resource.Drawable.red;
                    break;
                case Player.YellowPlayer:
                    imageResource = Resource.Drawable.yellow;
                    break;
            }
            return imageResource;
        }

        private void toggleActivePlayer()
        {
            _activePlayer = _activePlayer==Player.RedPlayer ?
                            Player.YellowPlayer :
                            Player.RedPlayer;
        }
    }

    public enum Player
    {
        RedPlayer,
        YellowPlayer
    }

    public enum BlockStatus
    {
        UnTaken,
        Red,
        Yellow
    }

    public enum Winner
    {
        RedPlayer,
        YellowPlayer,
        Non
    }
}

