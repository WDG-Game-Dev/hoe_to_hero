

//public class KertasInteraction : MonoBehaviour, IInteractable
//{
//    [SerializeField] private string isiKertas = "Ini adalah isi kertas.\nAda beberapa informasi penting di sini.";
//    [SerializeField] private Text uiText;  // UI Text untuk menampilkan isi kertas
//    [SerializeField] private GameObject uiPanel;  // Panel UI yang berisi Text
//    private bool isNearKertas = false;

//    private void Start()
//    {
//        uiPanel.SetActive(false);  // Pastikan UI tidak tampil saat awal
//    }

//    private void Update()
//    {
//        // Cek jika pemain berada di dekat kertas dan menekan tombol interaksi (misalnya "E")
//        if (isNearKertas && Input.GetKeyDown(KeyCode.E))
//        {
//            Interact();  // Tampilkan isi kertas
//        }
//    }

//    private void OnMouseEnter()
//    {
//        // Pemain berada dekat dengan kertas
//        isNearKertas = true;
//    }

//    private void OnMouseExit()
//    {
//        // Pemain menjauh dari kertas
//        isNearKertas = false;
//    }

//    public void Interact()
//    {
//        // Menampilkan isi kertas di UI
//        ShowIsiKertas();
//    }

//    public bool CanInteract()
//    {
//        // Hanya bisa berinteraksi jika pemain berada cukup dekat dengan kertas
//        return isNearKertas;
//    }

//    private void ShowIsiKertas()
//    {
//        // Menampilkan isi kertas di UI
//        uiPanel.SetActive(true);  // Tampilkan panel UI
//        uiText.text = isiKertas;  // Set text dengan isi kertas
//    }

//    public void CloseKertas()
//    {
//        // Menutup panel UI dan menyembunyikan isi kertas
//        uiPanel.SetActive(false);
//    }
//}

//using System.Collections;
//using TMPro;
//using UnityEngine;
//using UnityEngine.UI;
//public class KertasInteraction : MonoBehaviour, IInteractable
//{
//    public ObjectDialogue dialogueData;
//    public GameObject dialoguePanel;
//    public TMP_Text dialogueText, nameText;
//    public Image potraitImage;

//    private int dialogueIndex;
//    private bool isTyping, isDialogueActive;

//    public bool CanInteract()
//    {
//        return !isDialogueActive;
//    }

//    public void Interact()
//    {
//        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
//            return;
//        if(isDialogueActive)
//        {
//            NextLine();
//        }
//        else
//        {
//            StartDialogue();
//        }
//    }

//    void StartDialogue()
//    {
//        isDialogueActive = true;
//        dialogueIndex = 0;

//        nameText.SetText(dialogueData.name);
//        potraitImage.sprite = dialogueData.npcPotrait;

//        dialoguePanel.SetActive(true);
//        PauseController.SetPause(true);

//        StartCoroutine(TypeLine());
//    }

//    void NextLine()
//    {
//        if(isTyping)
//        {
//            StopAllCoroutines();
//            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
//            isTyping = false;
//        }
//        else if(++dialogueIndex < dialogueData.dialogueLines.Length)
//        {
//            //if another line,type next line
//            StartCoroutine(TypeLine());
//        }
//        else
//        {
//            EndDialogue();
//        }
//    }

//    IEnumerator = TypeLine()
//    {
//        isTyping = true;
//        dialogueText.SetText("");

//        foreach(char letter in dialogueData.dialogueLines[dialogueIndex])
//        {
//            dialogueText.text += letter;
//            yield return new WaitForSeconds(dialogueData.typingSpeed);
//        }

//        isTyping = false;
//        if(dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
//        {
//            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
//            NextLine();
//        }

//    }   

//    public void EndDialogue()
//    {
//        StopAllCoroutines();
//        isDialogueActive = false;
//        dialogueText.SetText("");
//        dialoguePanel.SetActive(false);
//        PauseController.SetPause(false);
//    }

//}


//using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using System.Collections;

//public class KertasInteraction : MonoBehaviour, IInteractable
//{
//    public ObjectDialogue dialogueData;
//    public GameObject dialoguePanel;
//    public TMP_Text dialogueText, nameText;
//    public Image potraitImage;

//    private int dialogueIndex;
//    private bool isTyping, isDialogueActive;

//    // Memastikan bahwa kita bisa berinteraksi hanya jika dialog tidak sedang aktif
//    public bool CanInteract()
//    {
//        return !isDialogueActive;
//    }

//    // Fungsi untuk memulai atau melanjutkan dialog
//    public void Interact()
//    {
//        if (dialogueData == null || (PauseController.IsGamePaused && !isDialogueActive))
//            return;

//        if (isDialogueActive)
//        {
//            NextLine();  // Melanjutkan ke baris berikutnya jika dialog aktif
//        }
//        else
//        {
//            StartDialogue();  // Memulai dialog jika belum aktif
//        }
//    }

//    // Fungsi untuk memulai dialog
//    void StartDialogue()
//    {
//        isDialogueActive = true;
//        dialogueIndex = 0;

//        // Menampilkan nama karakter dan potret NPC
//        nameText.SetText(dialogueData.npcName);
//        potraitImage.sprite = dialogueData.npcPotrait;

//        dialoguePanel.SetActive(true);  // Menampilkan panel dialog
//        PauseController.SetPause(true);  // Menjeda permainan

//        StartCoroutine(TypeLine());  // Memulai pengetikan baris pertama
//    }

//    // Fungsi untuk melanjutkan ke baris berikutnya
//    void NextLine()
//    {
//        if (isTyping)
//        {
//            // Jika masih mengetik, berhenti mengetik dan tampilkan teks lengkap
//            StopAllCoroutines();
//            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
//            isTyping = false;
//        }
//        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
//        {
//            // Jika masih ada baris dialog, lanjutkan mengetik
//            StartCoroutine(TypeLine());
//        }
//        else
//        {
//            // Jika semua dialog sudah selesai
//            EndDialogue();
//        }
//    }

//    // Coroutine untuk mengetik teks per karakter dengan efek typing
//    IEnumerator TypeLine()
//    {
//        isTyping = true;
//        dialogueText.SetText("");  // Kosongkan teks sebelumnya

//        // Mengetik teks karakter per karakter
//        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
//        {
//            dialogueText.text += letter;
//            yield return new WaitForSeconds(dialogueData.typingSpeed);
//        }

//        isTyping = false;

//        // Jika ada pengaturan auto progress (lanjutkan otomatis), maka tunggu beberapa detik dan lanjutkan ke baris berikutnya
//        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
//        {
//            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
//            NextLine();
//        }
//    }

//    // Fungsi untuk menutup dialog setelah selesai
//    public void EndDialogue()
//    {
//        StopAllCoroutines();  // Menghentikan semua coroutine
//        isDialogueActive = false;
//        dialogueText.SetText("");  // Menghapus teks
//        dialoguePanel.SetActive(false);  // Menyembunyikan panel dialog
//        PauseController.SetPause(false);  // Mengakhiri jeda permainan
//    }
//}




using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class KertasInteraction : MonoBehaviour, IInteractable
{
    public ObjectDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image potraitImage;

    private int dialogueIndex;
    private bool isTyping, isDialogueActive;
    private bool isNearKertas;

    // Memastikan bahwa kita bisa berinteraksi hanya jika dialog tidak sedang aktif
    //public bool CanInteract()
    //{
    //    return !isDialogueActive;  // Hanya bisa berinteraksi jika dialog tidak sedang aktif
    //}
    public bool CanInteract()
    {
        return isNearKertas && !isDialogueActive; // Pemain harus dekat dan dialog tidak aktif
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearKertas = true;  // Pemain memasuki area trigger kertas
            Debug.Log("Player can interact with the paper.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNearKertas = false; // Pemain keluar dari area trigger kertas
            Debug.Log("Player is no longer in range of the paper.");
        }
    }

    // Fungsi untuk memulai atau melanjutkan dialog
    public void Interact()
    {
        Debug.Log("Interacting with the paper.");
        if (dialogueData == null || PauseController.IsGamePaused) return;

        if (isDialogueActive)
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    // Fungsi untuk memulai dialog
    void StartDialogue()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        // Menampilkan nama karakter dan potret NPC
        nameText.SetText(dialogueData.name);
        potraitImage.sprite = dialogueData.npcPotrait;

        dialoguePanel.SetActive(true);  // Menampilkan panel dialog
        PauseController.SetPause(true);  // Menjeda permainan

        StartCoroutine(TypeLine());  // Memulai pengetikan baris pertama
    }

    // Fungsi untuk melanjutkan ke baris berikutnya
    void NextLine()
    {
        if (isTyping)
        {
            // Jika masih mengetik, berhenti mengetik dan tampilkan teks lengkap
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            // Jika masih ada baris dialog, lanjutkan mengetik
            StartCoroutine(TypeLine());
        }
        else
        {
            // Jika semua dialog sudah selesai
            EndDialogue();
        }
    }

    // Coroutine untuk mengetik teks per karakter dengan efek typing
    IEnumerator TypeLine()
    {
        isTyping = true;
        dialogueText.SetText("");  // Kosongkan teks sebelumnya

        // Mengetik teks karakter per karakter
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;

        // Jika ada pengaturan auto progress (lanjutkan otomatis), maka tunggu beberapa detik dan lanjutkan ke baris berikutnya
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds(dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    // Fungsi untuk menutup dialog setelah selesai
    public void EndDialogue()
    {
        StopAllCoroutines();  // Menghentikan semua coroutine
        isDialogueActive = false;
        dialogueText.SetText("");  // Menghapus teks
        dialoguePanel.SetActive(false);  // Menyembunyikan panel dialog
        PauseController.SetPause(false);  // Mengakhiri jeda permainan
    }
}
