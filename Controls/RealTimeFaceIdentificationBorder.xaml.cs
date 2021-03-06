﻿// 
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
// 
// Microsoft Cognitive Services: http://www.microsoft.com/cognitive
// 
// Microsoft Cognitive Services Github:
// https://github.com/Microsoft/Cognitive
// 
// Copyright (c) Microsoft Corporation
// All rights reserved.
// 
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using IntelligentKioskSample.Helper;
using Microsoft.ProjectOxford.Common.Contract;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace IntelligentKioskSample.Controls
{
    public sealed partial class RealTimeFaceIdentificationBorder : UserControl
    {
        public RealTimeFaceIdentificationBorder()
        {
            this.InitializeComponent();
        }

        public void ShowFaceRectangle(double left, double top, double width, double height)
        {
            this.faceRectangle.Margin = new Thickness(left, top, 0, 0);
            this.faceRectangle.Width = width;
            this.faceRectangle.Height = height;

            this.faceRectangle.Visibility = Visibility.Visible;
        }

        public void ShowRealTimeEmotionData(EmotionScores scores)
        {
            this.emotionEmojiControl.UpdateEmotion(scores);
        }
        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            hex = hex.Replace("#", string.Empty);
            byte a = (byte)(Convert.ToUInt32(hex.Substring(0, 2), 16));
            byte r = (byte)(Convert.ToUInt32(hex.Substring(2, 2), 16));
            byte g = (byte)(Convert.ToUInt32(hex.Substring(4, 2), 16));
            byte b = (byte)(Convert.ToUInt32(hex.Substring(6, 2), 16));
            SolidColorBrush myBrush = new SolidColorBrush(Windows.UI.Color.FromArgb(a, r, g, b));
            return myBrush;
        }
        public void ShowIdentificationData(double age, string gender, uint confidence, string name = null, string uniqueId = null)
        {
            if(age==1)
            {
                this.captionTextHeader.Text = name;
                //this.captionTextSubHeader.Text = "Tools";
                this.captionTextSubHeader.Text = uniqueId;

                this.captionBorder.Visibility = Visibility.Visible;
                this.captionBorder.Margin = new Thickness(this.faceRectangle.Margin.Left - (this.captionBorder.Width - this.faceRectangle.Width) / 2,
                                                        this.faceRectangle.Margin.Top - this.captionBorder.Height - 2, 0, 0);

                faceRectangle.Stroke = GetSolidColorBrush(Constants.PeachColor);

                faceRectangle.StrokeThickness = 4;
                OuterBackground.Background = GetSolidColorBrush(Constants.PeachColor);
            }
            else
            {
                if (!string.IsNullOrEmpty(name))
                {
                    if (name == "Mohammad")
                    {
                        string helmettext = "No Helmet";
                        if (Constants.isHelmet)
                            helmettext = "";

                        this.captionTextHeader.Text = string.Format("{0}, ({1})", name + " - Supervisor", helmettext);
                    }
                    else
                    {
                        string helmettext = "No Helmet";
                        if (Constants.isHelmet)
                            helmettext = "";
                        this.captionTextHeader.Text = string.Format("{0}, ({1})", name , helmettext);
                    }
                }
                else if (!string.IsNullOrEmpty(gender))
                {
                    this.captionTextHeader.Text = string.Format("{0}", "Unauthorized");
                    this.captionTextHeader.Foreground = new SolidColorBrush(Windows.UI.Colors.Red);
                }

                if (uniqueId != null)
                {
                    this.captionTextSubHeader.Text = string.Format("Face Id: {0}", uniqueId);
                }

                this.captionBorder.Visibility = Visibility.Visible;
                this.captionBorder.Margin = new Thickness(this.faceRectangle.Margin.Left - (this.captionBorder.Width - this.faceRectangle.Width) / 2,
                                                        this.faceRectangle.Margin.Top - this.captionBorder.Height - 2, 0, 0);
            }

          
        }
    }
}