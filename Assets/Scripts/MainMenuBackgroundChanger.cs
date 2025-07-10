using UnityEngine;
using UnityEngine.UI; // Wajib ada untuk mengakses komponen Image

public class MainMenuBackgroundChanger : MonoBehaviour
{
    // 1. Tempat untuk menaruh komponen Image dari Inspector
    public Image backgroundImage;

    // 2. List untuk menampung 4 gambar sprite Anda
    public Sprite[] backgroundImages;

    // 3. Interval waktu untuk berganti gambar (dalam detik)
    public float changeInterval = 20f;

    // Variabel internal untuk timer dan index gambar
    private float timer;
    private int currentImageIndex;

    void Start()
    {
        // Pastikan ada gambar di dalam list untuk menghindari error
        if (backgroundImages.Length > 0)
        {
            // Set gambar awal saat game dimulai
            backgroundImage.sprite = backgroundImages[0];
            currentImageIndex = 0;
        }
    }

    void Update()
    {
        // Jangan jalankan jika tidak ada gambar
        if (backgroundImages.Length == 0)
        {
            return;
        }

        // Tambahkan waktu yang berlalu sejak frame terakhir ke timer
        timer += Time.deltaTime;

        // Cek jika timer sudah mencapai interval yang ditentukan
        if (timer >= changeInterval)
        {
            // Reset timer kembali ke 0
            timer = 0f;

            // Pindah ke index gambar selanjutnya
            currentImageIndex++;

            // Jika index sudah melebihi jumlah gambar, kembali ke awal (index 0)
            // Ini adalah cara sederhana untuk membuat loop
            if (currentImageIndex >= backgroundImages.Length)
            {
                currentImageIndex = 0;
            }
            
            // Cara lebih singkat untuk loop menggunakan operator Modulo (%)
            // currentImageIndex = (currentImageIndex + 1) % backgroundImages.Length;

            // Ganti sprite pada komponen Image dengan gambar yang baru
            backgroundImage.sprite = backgroundImages[currentImageIndex];
        }
    }
}