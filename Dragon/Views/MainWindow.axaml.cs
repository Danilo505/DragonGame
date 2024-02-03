using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using NAudio.Wave;

namespace Dragon.Views;

public partial class MainWindow : Window
{
    private HashSet<Key> PressedKeys { get;} = new();
    private DispatcherTimer? timer;
    private WaveOutEvent waveOut;
    private AudioFileReader? audioFileReader;
    
    public MainWindow()
    {
        InitializeComponent();
        
        waveOut = new WaveOutEvent();
        
        KeyDown += HandleKeyDown;
        KeyUp += HandleKeyUp;
    }
    
    private void HandleKeyDown(object? sender, KeyEventArgs e)
    {
        PressedKeys.Add(e.Key);
        timer?.Stop();
        MovePlayer(sender, e);
    }

    private void HandleKeyUp(object? sender, KeyEventArgs e)
    {
        PressedKeys.Remove(e.Key);
        timer?.Stop();
    }

    public void MovePlayer(object sender, KeyEventArgs  e)
    {
        switch (e.Key)
        {
            case Key.Down:
                DragonDown();
                break;
            case Key.S:
                DragonDown();
                break;
            case Key.Up:
                DragonJump();
                break;
            case Key.W:
                DragonJump();
                break;
            case Key.Left:
                DragonFire();
                break;
            case Key.A:
                DragonFire();
                break;
            case Key.Right:
                DragonBlack();
                break;
            case Key.D:
                DragonBlack();
                break;
        }
        
    }

    public async void DragonDown()
    {
        
        LoadAudio("../../../Assets/Sounds/downsound.mp3");
        waveOut.Play();
        Character.Source = new Bitmap($"../../../Assets/Images/DragonLyingDown.png");
            
        await Task.Delay(260);
        
        waveOut.Stop();
        Character.Source = new Bitmap("../../../Assets/Images/DragonFlying.png");
    }

    public async void DragonJump()
    {
        LoadAudio(path:"../../../Assets/Sounds/jumpsound.mp3");
        waveOut.Play();
        Character.Source = new Bitmap("../../../Assets/Images/DragonJumping.png");
        Canvas.SetTop(Character, 0);

        await Task.Delay(260);
        
        waveOut.Stop();
        Character.Source = new Bitmap("../../../Assets/Images/DragonFlying.png");
        Canvas.SetTop(Character, 140);
    }
    
    public async void DragonFire()
    {
        LoadAudio(path:"../../../Assets/Sounds/firedragon.mp3");
        waveOut.Play();
        Character.Source = new Bitmap("../../../Assets/Images/DragonFire.png");
        Canvas.SetTop(Character, 0);

        await Task.Delay(360);
        
        waveOut.Stop();
        Character.Source = new Bitmap("../../../Assets/Images/DragonCrying.png");
        Canvas.SetTop(Character, 140);
    }
    
    public async void DragonBlack()
    {
        LoadAudio(path:"../../../Assets/Sounds/evolutiondragon.mp3");
        waveOut.Play();
        Character.Source = new Bitmap("../../../Assets/Images/DragonBlack.png");
        Canvas.SetTop(Character, 0);

        await Task.Delay(4000);
        
        waveOut.Stop();
        Character.Source = new Bitmap("../../../Assets/Images/DragonFlying.png");
        Canvas.SetTop(Character, 140);
    }
    
    public void LoadAudio(string path)
    {
        string audioFilePath = path;

        if (waveOut.PlaybackState == PlaybackState.Playing)
        {
            waveOut.Stop();
        }

        audioFileReader = new AudioFileReader(audioFilePath);
        waveOut.Init(audioFileReader);
    }
}