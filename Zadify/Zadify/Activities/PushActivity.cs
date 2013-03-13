using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using PushSharp.Client;
using Zadify.Push;

namespace Zadify.Activities
{
    [Activity(Label = "Push Registration")]
    public class PushActivity : Activity
    {
        const string TAG = "PushSharp-GCM";

        TextView _textRegistrationStatus;
        TextView _textRegistrationId;
        TextView _textLastMsg;
        Button _buttonRegister;
        bool _isRegistered;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.PushActivity);

            _textRegistrationStatus = FindViewById<TextView>(Resource.Id.textRegistrationStatus);
            _textRegistrationId = FindViewById<TextView>(Resource.Id.textRegistrationId);
            _textLastMsg = FindViewById<TextView>(Resource.Id.textLastMessage);
            _buttonRegister = FindViewById<Button>(Resource.Id.buttonRegister);

            PushClient.CheckDevice(this);
            PushClient.CheckManifest(this);


            _buttonRegister.Click += delegate
            {
                if (!_isRegistered)
                {
                    Log.Info(TAG, "Registering...");

                    PushClient.Register(this, PushHandlerBroadcastReceiver.SENDER_IDS);
                }
                else
                {
                    Log.Info(TAG, "Unregistering...");

                    PushClient.UnRegister(this);
                }

                RunOnUiThread(() =>
                {
                    _buttonRegister.Enabled = false;
                });
            };
        }

        protected override void OnResume()
        {
            base.OnResume();

            UpdateView();
        }

        void UpdateView()
        {
            var registrationId = PushClient.GetRegistrationId(this);

            if (string.IsNullOrEmpty(registrationId))
            {
                _isRegistered = false;
                _textRegistrationStatus.Text = "Registered: No";
                _textRegistrationId.Text = "Id: N/A";
                _buttonRegister.Text = "Register...";

                Log.Info(TAG, "Not registered...");
            }
            else
            {
                _isRegistered = true;
                _textRegistrationStatus.Text = "Registered: Yes";
                _textRegistrationId.Text = "Id: " + registrationId;
                _buttonRegister.Text = "Unregister...";

                Log.Info(TAG, "Already Registered: " + registrationId);
            }

            var prefs = GetSharedPreferences(PackageName, FileCreationMode.Private);
            _textLastMsg.Text = "Last Msg: " + prefs.GetString("last_msg", "N/A");

            _buttonRegister.Enabled = true;
        }
    }
}