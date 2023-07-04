using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using TMPro;

namespace mj
{
    [RequireComponent(typeof(RealtimeAvatarManager))]
    public class AvatarCounter : MonoBehaviour
    {
        private RealtimeAvatarManager _manager;
        public TextMeshProUGUI text;

        private void Awake()
        {
            _manager = GetComponent<RealtimeAvatarManager>();
            _manager.avatarCreated += AvatarCreated;
            _manager.avatarDestroyed += AvatarDestroyed;
        }

        private void AvatarCreated(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
        {
            // Avatar created!
            Debug.Log(_manager.avatars.Count);
            text.SetText(""+_manager.avatars.Count);

        }

        private void AvatarDestroyed(RealtimeAvatarManager avatarManager, RealtimeAvatar avatar, bool isLocalAvatar)
        {
            // Avatar destroyed!
            text.SetText("" + _manager.avatars.Count);
        }


    }
}

