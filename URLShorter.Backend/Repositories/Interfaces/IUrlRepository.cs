using URLShorter.Backend.Data.Entities;

namespace URLShorter.Backend.Repositories.Interfaces;

public interface IUrlRepository
{
    public void Insert(Url url);
    public void Remove(Url url);
}