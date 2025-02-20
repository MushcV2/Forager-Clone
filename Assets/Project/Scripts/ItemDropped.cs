using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropped : LifeController, IInteractable
{
    [Header("Drop Variables")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private float delayPerHit;
    [SerializeField] private int maxDrop;

    protected override void Start()
    {
        base.Start();

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (!canTakeDamage) return;

        TakeDamage(1);
        canTakeDamage = false;

        GivePlayerItems();
        CreateParticle();
        StartCoroutine(ObjectEffect());

        Invoke(nameof(DisableCooldown), delayPerHit);
    }

    private void CreateParticle()
    {
        GameObject _drop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Rigidbody2D _rb = _drop.GetComponent<Rigidbody2D>();

        _drop.GetComponent<SpriteRenderer>().sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        _drop.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

        _rb.velocity = new Vector2(Random.Range(-2, 2), Random.Range(1, 6));

        Destroy(_drop, 1);
    }

    private IEnumerator ObjectEffect()
    {
        Quaternion _initialRot = transform.rotation;
        Vector2 _initialScale = transform.localScale;

        transform.localScale = transform.localScale / 1.15f;
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(-25, 25));

        yield return new WaitForSeconds(0.25f);

        transform.localScale = _initialScale;
        transform.rotation = _initialRot;
    }

    private void GivePlayerItems()
    {
        int _rng = Random.Range(1, maxDrop + 1);

        player.GetComponent<PlayerInventory>().AddItem(gameObject, _rng);
    }

    private void DisableCooldown()
    {
        canTakeDamage = true;
    }

    protected override void Death()
    {
        base.Death();

        Destroy(gameObject);
    }
}