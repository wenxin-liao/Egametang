#ifndef RPC_RPCSESSION_H
#define RPC_RPCSESSION_H

#include <boost/asio.hpp>
#include <boost/enable_shared_from_this.hpp>
#include "Base/Marcos.h"
#include "Rpc/Typedef.h"
#include "Rpc/RpcCommunicator.h"

namespace Egametang {

class RpcServer;

class RpcSession: public RpcCommunicator, public std::enable_shared_from_this<RpcSession>
{
private:
	RpcServer& rpcServer;
	bool isStopped;

	virtual void OnRecvMessage(RpcMetaPtr meta, StringPtr message);
	virtual void OnSendMessage(RpcMetaPtr meta, StringPtr message);

public:
	RpcSession(boost::asio::io_service& ioService, RpcServer& server);
	~RpcSession();
	void Start();
	virtual void Stop();
};

} // namespace Egametang

#endif // RPC_RPCSESSION_H
