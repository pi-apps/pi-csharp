using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Newtonsoft.Json;
using RestSharp;
using stellar_dotnet_sdk.responses;
using stellar_dotnet_sdk;

namespace PiNetworkNet
{
    public class PiNetworkClient
    {
        private readonly RestClient _restClient = new RestClient("https://api.minepi.com/v2");
        private readonly string _apiKey;
        public PiNetworkClient(string apiKey)
        {
            _restClient.AddDefaultHeader("Content-Type", "application/json");
            //_restClient.AddDefaultHeader("Accept", "application/json");
            _apiKey = apiKey;
        }

        public async Task<PiAuthDto> Me(string accessToken)
        {
            var request = new RestRequest("/me");
            request.AddHeader("Accept", $"application/json");
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            request.Method = Method.Get;
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<PiAuthDto>(response.Content);                
            }
            else
            {
                try
                {
                    PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                    if (error != null)
                    {
                        throw new PiNetworkException()
                        {
                            PiError = error,
                        };
                    }
                    else
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
                catch
                {
                    throw new Exception($"{response.Content}");
                }
            }
        }

        public async Task<PaymentDto> Get(string identifier)
        {
            try
            {
                var request = new RestRequest($"/payments/{identifier}");
                request.AddHeader("Authorization", $"Key {_apiKey}");
                request.AddHeader("Accept", $"application/json");
                request.Method = Method.Get;
                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<PaymentDto>(response.Content);
                }
                else
                {
                    try
                    {
                        PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                        if (error != null)
                        {
                            throw new PiNetworkException()
                            {
                                PiError = error,
                            };
                        }
                        else
                        {
                            throw new Exception($"{response.Content}");
                        }
                    }
                    catch
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
            }
            catch { throw; }
        }

        public async Task<List<PaymentDto>> GetIncompleteServerPayments()
        {
            try
            {
                var request = new RestRequest("/payments/incomplete_server_payments");
                request.AddHeader("Accept", $"application/json");
                request.AddHeader("Authorization", $"Key {_apiKey}");
                request.Method = Method.Get;
                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var payments = JsonConvert.DeserializeObject<IncompleteServerPayments>(response.Content);
                    if (payments !=  null && payments.IncompletePayments != null && payments.IncompletePayments.Count > 0)
                        return payments.IncompletePayments;
                }
                else
                {
                    try
                    {
                        PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                        if (error != null)
                        {
                            throw new PiNetworkException()
                            {
                                PiError = error,
                            };
                        }
                        else
                        {
                            throw new Exception($"{response.Content}");
                        }
                    }
                    catch
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public async Task<PaymentDto> Create(CreatePaymentDto dto)
        {
            try
            {
                var request = new RestRequest($"/payments");
                request.AddHeader("Authorization", $"Key {_apiKey}");
                request.AddHeader("Accept", $"application/json");
                request.AddJsonBody(JsonConvert.SerializeObject(dto));
                request.Method = Method.Post;
                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    var payment = JsonConvert.DeserializeObject<PaymentDto>(response.Content);
                    if (payment != null 
                        && string.IsNullOrEmpty(payment.ToAddress) 
                        && !string.IsNullOrEmpty(payment.Identifier))
                    {
                        return await Get(payment.Identifier);
                    }
                    return payment;
                }
                else
                {
                    try
                    {
                        PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                        if (error != null)
                        {
                            throw new PiNetworkException()
                            {
                                PiError = error,
                            };
                        }
                        else
                        {
                            throw new Exception($"{response.Content}");
                        }
                    }
                    catch
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<PaymentDto> Approve(string identifier)
        {
            try
            {
                var request = new RestRequest($"/payments/{identifier}/approve");
                request.AddHeader("Accept", $"application/json");
                request.AddHeader("Authorization", $"Key {_apiKey}");
                request.Method = Method.Post;
                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<PaymentDto>(response.Content);
                }
                else
                {
                    try
                    {
                        PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                        if (error != null)
                        {
                            throw new PiNetworkException()
                            {
                                PiError = error,
                            };
                        }
                        else
                        {
                            throw new Exception($"{response.Content}");
                        }
                    }
                    catch
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
            }
            catch { throw; }
        }

        public async Task<PaymentDto> Cancel(string identifier)
        {
            try
            {
                var request = new RestRequest($"/payments/{identifier}/cancel");
                request.AddHeader("Accept", $"application/json");
                request.AddHeader("Authorization", $"Key {_apiKey}");
                request.Method = Method.Post;
                var response = await _restClient.ExecuteAsync(request);
                if (response.IsSuccessful)
                {
                    return JsonConvert.DeserializeObject<PaymentDto>(response.Content);
                }
                else
                {
                    try
                    {
                        PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                        if (error != null)
                        {
                            throw new PiNetworkException()
                            {
                                PiError = error,
                            };
                        }
                        else
                        {
                            throw new Exception($"{response.Content}");
                        }
                    }
                    catch
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
            }
            catch { throw; }
        }

        public async Task<PaymentDto> Complete(string identifier, string tx)
        {
            var request = new RestRequest($"/payments/{identifier}/complete");
            request.AddHeader("Accept", $"application/json");
            request.AddHeader("Authorization", $"Key {_apiKey}");
            var txid = new Tx() { TxId = tx };
            request.AddJsonBody(JsonConvert.SerializeObject(txid));
            request.Method = Method.Post;
            var response = await _restClient.ExecuteAsync(request);
            if (response.IsSuccessful)
            {
                return JsonConvert.DeserializeObject<PaymentDto>(response.Content);
            }
            else
            {
                try
                {
                    PiNetworkError error = JsonConvert.DeserializeObject<PiNetworkError>(response.Content);
                    if (error != null)
                    {
                        throw new PiNetworkException()
                        {
                            PiError = error,
                        };
                    }
                    else
                    {
                        throw new Exception($"{response.Content}");
                    }
                }
                catch
                {
                    throw new Exception($"{response.Content}");
                }
            }
        }


        protected async Task<Server> GetServerAsync(string network)
        {
            Server server;
            if (network == "Pi Network")
            {
                server = new Server("https://api.mainnet.minepi.com");
            }
            else if (network == "Pi Testnet")
            {
                server = new Server("https://api.testnet.minepi.com");
            }
            else
            {
                server = new Server("https://horizon-testnet.stellar.org");
            }
            await Task.CompletedTask;
            return server;
        }

        public async Task<double> GetAccountBalance(string network, string account)
        {
            //Set network and server
            Server server = await GetServerAsync(network);
            KeyPair keypair;

            //Generate a keypair from the account id.
            try
            {
                if (account.StartsWith("S"))
                {
                    keypair = KeyPair.FromSecretSeed(account);
                }
                else
                {
                    keypair = KeyPair.FromAccountId(account);
                }
            }
            catch
            {
                return 0.0;
            }

            //Load the account
            AccountResponse accountResponse = await server.Accounts.Account(keypair.AccountId);

            //Get the balance
            Balance[] balances = accountResponse.Balances;

            //Show the balance
            for (int i = 0; i < balances.Length; i++)
            {
                Balance asset = balances[i];
                Console.WriteLine("Asset Code: " + asset.AssetCode);
                Console.WriteLine("Asset Amount: " + asset.BalanceString);
                if (asset.AssetType == "native")
                {
                    return double.Parse(asset.BalanceString);
                }
            }
            return 0.0;
        }

        public async Task<SubmitTransactionResponse> SendNativeAssets(string network, string seed, TransactionData data, uint fee = 100000)
        {
            //Source keypair from the secret seed
            KeyPair sourceKeypair = KeyPair.FromSecretSeed(seed);
            return await SendNativeAssets(network, sourceKeypair, data, fee);
        }

        public async Task<SubmitTransactionResponse> SendNativeAssets(string network, KeyPair sourceKeypair, TransactionData data, uint fee = 100000)
        {
            //Set network and server
            Server server = await GetServerAsync(network);

            //Destination keypair from the account id
            KeyPair destinationKeyPair = KeyPair.FromAccountId(data.ToAddress);

            //Load source account data
            AccountResponse sourceAccountResponse = await server.Accounts.Account(sourceKeypair.AccountId);

            //Create source account object
            Account sourceAccount = new Account(sourceKeypair.AccountId, sourceAccountResponse.SequenceNumber);

            //Create asset object with specific amount
            //You can use native or non native ones.
            Asset asset = new AssetTypeNative();
            double balance = 0.0;
            for (int i = 0; i < sourceAccountResponse.Balances.Length; i++)
            {
                Balance ast = sourceAccountResponse.Balances[i];
                if (ast.AssetType == "native")
                {
                    if (double.TryParse(ast.BalanceString, out balance))
                        break;
                }
            }
            if (balance < data.Amount + 0.01)
            {
                throw new Exception($"Not enough balance ({balance})");
            }
            string amount = $"{Math.Floor(data.Amount * 10000000.0)/ 10000000.0:F7}";
            try
            {
                //Create payment operation
                PaymentOperation operation = new PaymentOperation.Builder(destinationKeyPair, asset, amount)
                                                    .SetSourceAccount(sourceAccount.KeyPair)
                                                    .Build();

                var Identifier = string.IsNullOrEmpty(data.Identifier) ? $"" : $"{data.Identifier.Trim()}";
                MemoText memo = new MemoText(string.IsNullOrEmpty(Identifier) ? $"" : Identifier.Substring(0, Math.Min(Identifier.Length, 28)));
                //Create transaction and add the payment operation we created
                Transaction transaction = new TransactionBuilder(sourceAccount)
                                            .AddOperation(operation)
                                            .AddMemo(memo)
                                            .SetFee(fee)
                                            .Build();

                //Sign Transaction
                transaction.Sign(sourceKeypair, new Network(network));

                //Try to send the transaction
                var tx = await server.SubmitTransaction(transaction);
                return tx;
            }
            catch (Exception exception)
            {
                Console.WriteLine("Send Transaction Failed");
                Console.WriteLine("Exception: " + exception.Message);
                throw;
            }
        }
    }
}
