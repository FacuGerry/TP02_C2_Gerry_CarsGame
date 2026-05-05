using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionScreen : MonoBehaviour
{
    [Header("Cars")]
    [SerializeField] private SelectionsSO _selection;
    [SerializeField] private CarSettingsSO[] _carsList = new CarSettingsSO[0];

    [Header("Settings for visualizing cars")]
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private UiSelectionScreenButtons _uiSelectionScreenButtons;

    [Header("Tracks")]
    [SerializeField] private TrackSettingsSO[] _tracksList = new TrackSettingsSO[0];

    private List<GameObject> _carsVisuals = new List<GameObject>();
    private int _indexCar = -1;

    private List<GameObject> _trackVisuals = new List<GameObject>();
    private int _indexTracks = -1;

    private void Start()
    {
        GameObject carParent = new GameObject("Car parent");
        GameObject trackParent = new GameObject("Track parent");

        _indexCar = 0;
        _indexTracks = 0;
   
        foreach (CarSettingsSO car in _carsList)
            _carsVisuals.Add(Instantiate(car.visual, carParent.transform));
        
        foreach (TrackSettingsSO track in _tracksList)
            _trackVisuals.Add(Instantiate(track.visual, trackParent.transform));
    }

    private void OnEnable()
    {
        _uiSelectionScreenButtons.OnVehiclePressed += ChangeCar;

        _uiSelectionScreenButtons.OnTrackPressed += ChangeTrack;

        _uiSelectionScreenButtons.OnPlayPressed += SetSelections;
    }

    private void Update()
    {
        RotateCar(_carsVisuals[_indexCar]);
    }

    private void OnDisable()
    {
        _uiSelectionScreenButtons.OnVehiclePressed -= ChangeCar;

        _uiSelectionScreenButtons.OnTrackPressed -= ChangeTrack;

        _uiSelectionScreenButtons.OnPlayPressed -= SetSelections;
    }

    // CAR SETTINGS

    private void RotateCar(GameObject go)
    {
        float rotation = _rotationSpeed * Time.deltaTime;
        go.transform.localEulerAngles += new Vector3(0f, rotation, 0f);
    }

    private void ChangeCar(bool goRight)
    {
        _carsVisuals[_indexCar].SetActive(false);

        if (goRight)
        {
            _indexCar++;
            if (_indexCar > _carsList.Length - 1)
                _indexCar = 0;
        }
        else
        {
            _indexCar--;
            if (_indexCar < 0)
                _indexCar = _carsList.Length - 1;
        }

        _carsVisuals[_indexCar].SetActive(true);
        Debug.Log("car changed");
    }

    // TRACK SETTINGS

    private void ChangeTrack(bool goRight)
    {
        _trackVisuals[_indexTracks].SetActive(false);

        if (goRight)
        {
            _indexTracks++;
            if (_indexTracks > _tracksList.Length - 1)
                _indexTracks = 0;
        }
        else
        {
            _indexTracks--;
            if (_indexTracks < 0)
                _indexTracks = _tracksList.Length - 1;
        }

        _trackVisuals[_indexTracks].SetActive(true);
        Debug.Log("track changed");
    }

    // SET EVERYTHING

    private void SetSelections()
    {
        _selection.car = _carsList[_indexCar];
        _selection.track = _tracksList[_indexTracks];

        SceneManager.LoadScene(_selection.track.sceneName);
    }
}
