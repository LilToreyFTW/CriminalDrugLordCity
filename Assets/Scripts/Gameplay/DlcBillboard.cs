using UnityEngine;

namespace CriminalDrugLordCity.Gameplay
{
    public sealed class DlcBillboard : MonoBehaviour
    {
        private string _title = "";
        private string _season = "";
        private string _summary = "";
        private Camera _camera;

        public void SetContent(string title, string season, string summary)
        {
            _title = title;
            _season = season;
            _summary = summary;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnGUI()
        {
            if (_camera == null)
            {
                _camera = Camera.main;
                if (_camera == null)
                {
                    return;
                }
            }

            Vector3 screen = _camera.WorldToScreenPoint(transform.position + Vector3.up * 4f);
            if (screen.z <= 0f)
            {
                return;
            }

            Rect rect = new Rect(screen.x - 120f, Screen.height - screen.y - 80f, 240f, 70f);
            GUI.Box(rect, _title + "\n" + _season);
        }
    }
}
