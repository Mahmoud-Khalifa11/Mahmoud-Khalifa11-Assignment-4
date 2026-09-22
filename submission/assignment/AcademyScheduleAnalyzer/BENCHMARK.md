# Benchmark Results

Run the benchmark on your own machine:

```bash
dotnet run -c Release -- benchmark
```

Paste the actual BenchmarkDotNet output/table here.

## Required columns
- Method
- Mean
- Error
- StdDev
- Allocated

## Questions

### 1. Which approach was faster?
Answer from your actual machine results.

### 2. Which approach allocated more memory?
Answer from the `Allocated` column.

### 3. Did the difference change as Iterations increased?
Compare the rows for 100, 1,000, 10,000 and 100,000 iterations.

Do not copy benchmark numbers from another machine.
